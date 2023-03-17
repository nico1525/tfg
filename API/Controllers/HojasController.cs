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
    public class HojasController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public HojasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Hojas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hoja>>> GetHoja()
        {
          if (_context.Hoja == null)
          {
              return NotFound();
          }
            return await _context.Hoja.ToListAsync();
        }

        // GET: api/Hojas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hoja>> GetHoja(int id)
        {
          if (_context.Hoja == null)
          {
              return NotFound();
          }
            var hoja = await _context.Hoja.FindAsync(id);

            if (hoja == null)
            {
                return NotFound();
            }

            return hoja;
        }

        // PUT: api/Hojas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoja(int id, Hoja hoja)
        {
            if (id != hoja.Id)
            {
                return BadRequest();
            }

            _context.Entry(hoja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HojaExists(id))
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

        // POST: api/Hojas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hoja>> PostHoja(Hoja hoja)
        {
          if (_context.Hoja == null)
          {
              return Problem("Entity set 'ItemContext.Hoja'  is null.");
          }
            _context.Hoja.Add(hoja);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoja", new { id = hoja.Id }, hoja);
        }

        // DELETE: api/Hojas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoja(int id)
        {
            if (_context.Hoja == null)
            {
                return NotFound();
            }
            var hoja = await _context.Hoja.FindAsync(id);
            if (hoja == null)
            {
                return NotFound();
            }

            _context.Hoja.Remove(hoja);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HojaExists(int id)
        {
            return (_context.Hoja?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
