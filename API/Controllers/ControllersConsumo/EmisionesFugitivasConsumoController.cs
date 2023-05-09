using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Consumos;
using API.Models.Context;

namespace API.Controllers.ControllersConsumo
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisionesFugitivasConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmisionesFugitivasConsumoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/EmisionesFugitivasConsumoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmisionesFugitivasConsumo>>> GetEmisionesFugitivasConsumo()
        {
          if (_context.EmisionesFugitivasConsumo == null)
          {
              return NotFound();
          }
            return await _context.EmisionesFugitivasConsumo.ToListAsync();
        }

        // GET: api/EmisionesFugitivasConsumoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmisionesFugitivasConsumo>> GetEmisionesFugitivasConsumo(int id)
        {
          if (_context.EmisionesFugitivasConsumo == null)
          {
              return NotFound();
          }
            var emisionesFugitivasConsumo = await _context.EmisionesFugitivasConsumo.FindAsync(id);

            if (emisionesFugitivasConsumo == null)
            {
                return NotFound();
            }

            return emisionesFugitivasConsumo;
        }

        // PUT: api/EmisionesFugitivasConsumoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmisionesFugitivasConsumo(int id, EmisionesFugitivasConsumo emisionesFugitivasConsumo)
        {
            if (id != emisionesFugitivasConsumo.Id)
            {
                return BadRequest();
            }

            _context.Entry(emisionesFugitivasConsumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmisionesFugitivasConsumoExists(id))
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

        // POST: api/EmisionesFugitivasConsumoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmisionesFugitivasConsumo>> PostEmisionesFugitivasConsumo(EmisionesFugitivasConsumo emisionesFugitivasConsumo)
        {
          if (_context.EmisionesFugitivasConsumo == null)
          {
              return Problem("Entity set 'DatabaseContext.EmisionesFugitivasConsumo'  is null.");
          }
            _context.EmisionesFugitivasConsumo.Add(emisionesFugitivasConsumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmisionesFugitivasConsumo", new { id = emisionesFugitivasConsumo.Id }, emisionesFugitivasConsumo);
        }

        // DELETE: api/EmisionesFugitivasConsumoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmisionesFugitivasConsumo(int id)
        {
            if (_context.EmisionesFugitivasConsumo == null)
            {
                return NotFound();
            }
            var emisionesFugitivasConsumo = await _context.EmisionesFugitivasConsumo.FindAsync(id);
            if (emisionesFugitivasConsumo == null)
            {
                return NotFound();
            }

            _context.EmisionesFugitivasConsumo.Remove(emisionesFugitivasConsumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmisionesFugitivasConsumoExists(int id)
        {
            return (_context.EmisionesFugitivasConsumo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
