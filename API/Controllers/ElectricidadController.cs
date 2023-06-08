using System;
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
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    [Route("api/Organizacion/Electricidad")]
    [ApiController]
    public class ElectricidadController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ElectricidadController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetElectricidad()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<Electricidad> listatodoselectricidads = await _context.Electricidad.ToListAsync();
            List<ElectricidadDTO> listaelectricidadsorg = new();

            foreach (var electricidad in listatodoselectricidads)
            {
                if (electricidad.OrganizacionId == currentUser.OrganizacionId)
                {
                    ElectricidadDTO electricidadDTO = _mapper.Map<ElectricidadDTO>(electricidad);
                    listaelectricidadsorg.Add(electricidadDTO);
                }
            }

            if (currentUser.Role != Role.SuperAdmin) { return listaelectricidadsorg; }
            else { return listatodoselectricidads; }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricidadDTO>> GetElectricidadById(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Electricidad Electricidad = await _context.Electricidad.FindAsync(id);
            ElectricidadDTO ElectricidadDTO;
            if (Electricidad.OrganizacionId == currentUser.OrganizacionId)
            {
                ElectricidadDTO = _mapper.Map<ElectricidadDTO>(Electricidad);
            }
            else
            {
                return BadRequest("Esta Dispositivo no existe o no pertenece a esta organización");
            }
            return ElectricidadDTO;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricidad(int id, ElectricidadModifyDTO electricidad)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var electricidadChange = await _context.Electricidad.FindAsync(id);
                if (currentUser.OrganizacionId != electricidadChange.OrganizacionId)
                {
                    return BadRequest("Esta dispositivo no existe o no pertenece a esta organización");
                }

                if (electricidad.Edificio != null && electricidad.Edificio != "") electricidadChange.Edificio = electricidad.Edificio;
                if (electricidad.Dispositivo != null && electricidad.Dispositivo != "") electricidadChange.Dispositivo = electricidad.Dispositivo;
                if (electricidad.Descripcion != null && electricidad.Descripcion != "") electricidadChange.Descripcion = electricidad.Descripcion;

                await _context.SaveChangesAsync();

                return Ok("Dispositivo modificada correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ninguna dispositivo");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Electricidad>> PostElectricidad(ElectricidadCreateDTO electricidadDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Electricidad electricidad = _mapper.Map<Electricidad>(electricidadDTO);

            electricidad.OrganizacionId = currentUser.OrganizacionId;
            _context.Electricidad.Add(electricidad);
            await _context.SaveChangesAsync();
            return Ok("Dispositivo creado correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricidad(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var electricidadDelete = await _context.Electricidad.FindAsync(id);
                if (currentUser.OrganizacionId != electricidadDelete.OrganizacionId)
                {
                    return BadRequest("Esta dispositivo no existe o no pertenece a esta organización");
                }
                _context.Electricidad.Remove(electricidadDelete);
                await _context.SaveChangesAsync();

                return Ok("Dispositivo eliminado correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún dispositivo");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllElectricidads()
        {
            List<Electricidad> electricidadlist = await _context.Electricidad.ToListAsync();

            foreach (var electricidad in electricidadlist)
            {
                _context.Electricidad.Remove(electricidad);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas los dispositivos eliminados correctamente");
        }
    }
}
