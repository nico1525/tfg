using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Net;
using System.Security.Policy;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using API.Models.DTOs.Outcoming;
using API.Models.DTOs.Incoming;
using API.Models.Context;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/Organizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizacion>>> GetAllOrganizaciones()
        {
          if (_context.Organizacion == null)
          {
              return NotFound();
          }
            return await _context.Organizacion.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizacionDTO>> GetOrganizacion(int id)
        {
          if (_context.Organizacion == null)
          {
              return NotFound();
          }
            var organizacion = await _context.Organizacion.FindAsync(id);

            if (organizacion == null)
            {
                return NotFound("No existe la organización con id " + id);
            }
            OrganizacionDTO orgDTO = _mapper.Map<OrganizacionDTO>(organizacion);

            return orgDTO;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizacion(int id, Organizacion organizacion)
        {
            if (id != organizacion.Id)
            {
                return BadRequest("Este id no pertenece a la organización");
            }
            var antiguaorganizacion = await _context.Organizacion.FindAsync(id);

            if (antiguaorganizacion is null) { return NotFound("No existe la organización con id " + id); }

            if(_context.Organizacion.Any(e => e.NombreOrg == organizacion.NombreOrg))
            {
                return BadRequest("Ya existe una organización con ese nombre");
            }
            else
            {
                antiguaorganizacion.NombreOrg = organizacion.NombreOrg;
            }
            if (_context.Organizacion.Any(e => e.Email == organizacion.Email))
            {
                return BadRequest("Ya existe una organización con ese email");
            }
            else
            {
                antiguaorganizacion.Email = organizacion.Email;
            }
            antiguaorganizacion.Direccion = organizacion.Direccion;
            antiguaorganizacion.Contraseña = organizacion.Contraseña;

            await _context.SaveChangesAsync();

            return Ok("Organización modificada correctamente");
        }

        
        [HttpPost]
        public async Task<ActionResult<Organizacion>> PostOrganizacion(OrganizacionCreateDTO organizacion)
        {
            if (_context.Organizacion == null)
            {
              return Problem("Entity set 'DatabaseContext.Organizacion' is null.");
            }
            if (_context.Organizacion.Any(o => o.NombreOrg == organizacion.NombreOrg))
                return StatusCode(400, "Este nombre de esta Organización ya está registrado");
            if (_context.Organizacion.Any(o => o.Email == organizacion.Email))
                return StatusCode(400, "Este email ya está registrado");

            Organizacion org = _mapper.Map<Organizacion>(organizacion);

            _context.Organizacion.Add(org);
            await _context.SaveChangesAsync();

            return Ok("Organización creada correctamente");
        }      


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizacion(int id)
        {
            if (_context.Organizacion == null)
            {
                return NotFound();
            }
            var organizacion = await _context.Organizacion.FindAsync(id);
            if (organizacion == null)
            {
                return BadRequest("No existe una rutina con este id");
            }

            _context.Organizacion.Remove(organizacion);
            await _context.SaveChangesAsync();

            return Ok("Organización eliminada correctamente"); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllOrgs(int id)
        {
            if (_context.Organizacion == null)
            {
                return NotFound();
            }

            List<Organizacion> listorgs =  await _context.Organizacion.ToListAsync();

            foreach (var org in listorgs)
            {
                _context.Organizacion.Remove(org);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas las organizaciones eliminadas correctamente");
        }
    }
}
