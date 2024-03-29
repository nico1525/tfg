﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;
using API.Authorization;
using API.Helpers;
using API.Models.Autentificacion;
using AutoMapper;
using API.Models.Query_Models;

namespace API.Controllers
{
    [Route("api/Organizacion/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private IJwtUtils _jwtUtils;
        public UsuarioController(DatabaseContext context, IMapper mapper, IJwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        [Authorize]
        [HttpGet]
        public InfoUsuario GetUsuarios()
        {// Informacion del usuario junto a datos de la Organización
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

                InfoUsuario query = (from u in _context.Usuario
                             join o in _context.Organizacion
                             on u.OrganizacionId equals o.Id
                             select new InfoUsuario()
                             {
                                 Id = u.Id,
                                 NombreApellidos = u.NombreApellidos,
                                 Email = u.Email,
                                 Contraseña = u.Contraseña,
                                 Rol = u.Role,
                                 NombreOrg = o.NombreOrg,
                                 Dirección = o.Direccion
                             }).Where(r => r.Id == currentUser.Id).Single();
                return query;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioById(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Usuario usuario = await _context.Usuario.FindAsync(id);
            UsuarioDTO usuarioDTO;
            if (usuario.OrganizacionId == currentUser.OrganizacionId)
            {
                usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            }
            else
            {
                return BadRequest("Este usuario no existe o no pertenece a esta organización");
            }
            return usuarioDTO;

        }
        [Authorize(Role.OrgAdmin,Role.SuperAdmin)]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsuarios()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (currentUser.Role == Role.OrgAdmin)
            {
                List<UsuarioDTO> listusers = new();
                var allusers = await _context.Usuario.ToListAsync();
                foreach (var user in allusers)
                { 
                    if(user.OrganizacionId == currentUser.OrganizacionId)
                    {
                        UsuarioDTO userDTO = _mapper.Map<UsuarioDTO>(user);
                        listusers.Add(userDTO);
                    }
                }
                return listusers;
            }
            else
            {
                return await _context.Usuario.ToListAsync();
            }
        }

        [Authorize(Role.OrgAdmin, Role.User)]
        [HttpPut]
        public async Task<IActionResult> PutUsuario(UsuarioModifyDTO usuario)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (_context.Usuario.Any(e => e.Email == usuario.Email && e.Id != currentUser.Id))
            {
                return BadRequest("Ya existe un usuario con ese email");
            }
            else
            {
                if (usuario.Email != null && usuario.Email != "") currentUser.Email = usuario.Email;
            }
            if (usuario.NombreApellidos != null && usuario.NombreApellidos != "") currentUser.NombreApellidos = usuario.NombreApellidos;
            if (usuario.Contraseña != null && usuario.Contraseña != "") currentUser.Contraseña = usuario.Contraseña;

            await _context.SaveChangesAsync();

            return Ok("Usuario modificado correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioById(int id, UsuarioModifyDTO usuario)
        {
            try { 
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var userChange = await _context.Usuario.FindAsync(id);
            if (currentUser.OrganizacionId != userChange.OrganizacionId) {
                return BadRequest("Este usuario no pertenece a esta organización");
            }
            if (_context.Usuario.Any(e => e.Email == usuario.Email && e.Id != userChange.Id))
            {
                return BadRequest("Ya existe un usuario con ese email");
            }
            else
            {
                if (usuario.Email != null && usuario.Email != "") userChange.Email = usuario.Email;
            }
            if (usuario.NombreApellidos != null && usuario.NombreApellidos != "") userChange.NombreApellidos = usuario.NombreApellidos;
            if (usuario.Contraseña != null && usuario.Contraseña != "") userChange.Contraseña = usuario.Contraseña;
            if (usuario.Role == Role.User || usuario.Role == Role.OrgAdmin) userChange.Role = usuario.Role; 
                else
                {
                    return BadRequest("Seleccione un rol Válido");
                }

                await _context.SaveChangesAsync();

            return Ok("Usuario modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún usuario");
            }
        }

        [Authorize(Role.OrgAdmin)]
        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> RegistrarUsuario(UsuarioCreateDTO usuario)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (_context.Usuario.Any(o => o.Email == usuario.Email))
                return StatusCode(400, "Este email ya está registrado");

            Usuario usu = _mapper.Map<Usuario>(usuario);
            usu.OrganizacionId = currentUser.OrganizacionId;
            _context.Usuario.Add(usu);
            await _context.SaveChangesAsync();

            return Ok("Usuario creado correctamente");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<Usuario> LoginUsuario(LoginRequest loginData)
        {

            var user = _context.Usuario.SingleOrDefault(x => x.Email == loginData.Email);

            // validate
            if (user == null || loginData.Contraseña != user.Contraseña)
                return BadRequest("Usuario incorrecto");

            // authentication successful
            var response = _mapper.Map<LoginUserResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return Ok(response);
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioById(int id)
        {
            try { 
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var userDelete = await _context.Usuario.FindAsync(id);
            if (currentUser.OrganizacionId != userDelete.OrganizacionId)
            {
                return BadRequest("Este usuario no pertenece a esta organización");
            }
            if(userDelete.Role != Role.User)
            {
                return BadRequest("No puedes borrar un usuario que tenga un rol de administrador dentro de la organización");

            }
            _context.Usuario.Remove(userDelete);
            await _context.SaveChangesAsync();

            return Ok("Usuario eliminado correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún usuario");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllUsuarios()
        {
            List<Usuario> listorgs = await _context.Usuario.ToListAsync();

            foreach (var org in listorgs)
            {
                _context.Usuario.Remove(org);
            }
            await _context.SaveChangesAsync();

            return Ok("Todos los usuarios eliminados correctamente");
        }

    }
}
