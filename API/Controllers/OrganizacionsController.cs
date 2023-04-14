using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OrganizacionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Organizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizacion>>> GetOrganizacion()
        {
          if (_context.Organizacion == null)
          {
              return NotFound();
          }
            return await _context.Organizacion.ToListAsync();
        }

        // GET: api/Organizacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizacion>> GetOrganizacion(int id)
        {
          if (_context.Organizacion == null)
          {
              return NotFound();
          }
            var organizacion = await _context.Organizacion.FindAsync(id);

            if (organizacion == null)
            {
                return NotFound();
            }

            return organizacion;
        }

        // PUT: api/Organizacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizacion(int id, Organizacion organizacion)
        {
            if (id != organizacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(organizacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Organizacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organizacion>> PostOrganizacion(Organizacion organizacion)
        {
          if (_context.Organizacion == null)
          {
              return Problem("Entity set 'DatabaseContext.Organizacion'  is null.");
          }
            _context.Organizacion.Add(organizacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizacion", new { id = organizacion.Id }, organizacion);
        }

        // DELETE: api/Organizacions/5
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
                return NotFound();
            }

            _context.Organizacion.Remove(organizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizacionExists(int id)
        {
            return (_context.Organizacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
