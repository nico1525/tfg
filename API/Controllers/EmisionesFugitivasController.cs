using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisionesFugitivasController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmisionesFugitivasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/EmisionesFugitivas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmisionesFugitivas>>> GetEmisionesFugitivas()
        {
          if (_context.EmisionesFugitivas == null)
          {
              return NotFound();
          }
            return await _context.EmisionesFugitivas.ToListAsync();
        }

        // GET: api/EmisionesFugitivas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmisionesFugitivas>> GetEmisionesFugitivas(int id)
        {
          if (_context.EmisionesFugitivas == null)
          {
              return NotFound();
          }
            var emisionesFugitivas = await _context.EmisionesFugitivas.FindAsync(id);

            if (emisionesFugitivas == null)
            {
                return NotFound();
            }

            return emisionesFugitivas;
        }

        // PUT: api/EmisionesFugitivas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmisionesFugitivas(int id, EmisionesFugitivas emisionesFugitivas)
        {
            if (id != emisionesFugitivas.Id)
            {
                return BadRequest();
            }

            _context.Entry(emisionesFugitivas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmisionesFugitivasExists(id))
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

        // POST: api/EmisionesFugitivas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmisionesFugitivas>> PostEmisionesFugitivas(EmisionesFugitivas emisionesFugitivas)
        {
          if (_context.EmisionesFugitivas == null)
          {
              return Problem("Entity set 'DatabaseContext.EmisionesFugitivas'  is null.");
          }
            _context.EmisionesFugitivas.Add(emisionesFugitivas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmisionesFugitivas", new { id = emisionesFugitivas.Id }, emisionesFugitivas);
        }

        // DELETE: api/EmisionesFugitivas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmisionesFugitivas(int id)
        {
            if (_context.EmisionesFugitivas == null)
            {
                return NotFound();
            }
            var emisionesFugitivas = await _context.EmisionesFugitivas.FindAsync(id);
            if (emisionesFugitivas == null)
            {
                return NotFound();
            }

            _context.EmisionesFugitivas.Remove(emisionesFugitivas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmisionesFugitivasExists(int id)
        {
            return (_context.EmisionesFugitivas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
