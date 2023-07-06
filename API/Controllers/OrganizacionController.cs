using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using AutoMapper;
using API.Models.Context;
using API.Authorization;
using API.Models.Autentificacion;
using API.Helpers;
using System.ComponentModel.DataAnnotations;
using API.Models.Consumos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrganizacionController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetOrganizaciones()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (currentUser.Role == Role.OrgAdmin || currentUser.Role == Role.User)
            {
                var org = await _context.Organizacion.FindAsync(currentUser.OrganizacionId);
                OrganizacionDTO orgDTO = _mapper.Map<OrganizacionDTO>(org);
                List<OrganizacionDTO> listorg = new()
            {
                    orgDTO
            };
                return listorg;
            }
            else
            {
                return await _context.Organizacion.ToListAsync();
            }

        }

       
        [Authorize(Role.OrgAdmin)]
        [HttpPut]
        public async Task<IActionResult> PutOrganizacion(OrganizacionModifyDTO organizacion)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var currentOrg =  await _context.Organizacion.FindAsync(currentUser.Id);

            var antiguaorganizacion = await _context.Organizacion.FindAsync(currentOrg.Id);

            if (_context.Organizacion.Any(e => e.NombreOrg == organizacion.NombreOrg && e.Id != currentOrg.Id))
            {
                return BadRequest("Ya existe una organización con ese nombre");
            }
            else
            {
                if(organizacion.NombreOrg != null && organizacion.NombreOrg != "") currentOrg.NombreOrg = organizacion.NombreOrg;
            }
            if (_context.Organizacion.Any(e => e.Email == organizacion.Email && e.Id != currentOrg.Id))
            {
                return BadRequest("Ya existe una organización con ese email");
            }
            else
            {
                if (organizacion.Email != null && organizacion.Email != "") currentOrg.Email = organizacion.Email;
            }
            if (organizacion.Direccion != null && organizacion.Direccion != "") currentOrg.Direccion = organizacion.Direccion;
            if (organizacion.Contraseña != null && organizacion.Contraseña != "") currentOrg.Contraseña = organizacion.Contraseña;

            await _context.SaveChangesAsync();

            return Ok("Organización modificada correctamente");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Organizacion>> RegistrarOrganizacion(OrganizacionCreateDTO organizacion)
        {

            if (_context.Organizacion.Any(o => o.NombreOrg == organizacion.NombreOrg))
                return StatusCode(400, "Este nombre de esta Organización ya está registrado");
            if (_context.Organizacion.Any(o => o.Email == organizacion.Email))
                return StatusCode(400, "Este email de Organización ya está registrado");

            Organizacion org = _mapper.Map<Organizacion>(organizacion);


            _context.Organizacion.Add(org);
            await _context.SaveChangesAsync();

            Usuario usuario = new()
            {
                NombreApellidos = "admin",
                Email = org.Email,
                Contraseña = org.Contraseña,
                OrganizacionId = org.Id,
                Role = Role.OrgAdmin
            };
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Organización creada correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrganizacion()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var currentOrg = await _context.Organizacion.FindAsync(currentUser.OrganizacionId);
        
            _context.Organizacion.Remove(currentOrg);
            await _context.SaveChangesAsync();

            return Ok("Organización eliminada correctamente"); 
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllOrgs()
        {
            List<Organizacion> listorgs =  await _context.Organizacion.ToListAsync();

            foreach (var org in listorgs)
            {
                _context.Organizacion.Remove(org);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas las organizaciones eliminadas correctamente");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizacion>> DeleteOrganizacionById(int id)
        {
            try { 
            var organizacion = await _context.Organizacion.FindAsync(id);

            if (organizacion == null)
            {
                return NotFound("No existe la organización con id " + id);
            }

            return organizacion;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna organización");
            }
        }

    }
}
