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
    [Route("api/Organizacion/Dispositivo")]
    [ApiController]
    public class DispositivoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DispositivoController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispositivo>>> GetDispositivo()
        {
          if (_context.Dispositivo == null)
          {
              return NotFound();
          }
            return await _context.Dispositivo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dispositivo>> GetDispositivo(int id)
        {
          if (_context.Dispositivo == null)
          {
              return NotFound();
          }
            var dispositivo = await _context.Dispositivo.FindAsync(id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            return dispositivo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivo(int id, Dispositivo dispositivo)
        {
            if (id != dispositivo.Id)
            {
                return BadRequest();
            }

            _context.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivoExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Dispositivo>> PostDispositivo(Dispositivo dispositivo)
        {
          if (_context.Dispositivo == null)
          {
              return Problem("Entity set 'DatabaseContext.Dispositivo'  is null.");
          }
            List<Organizacion> listorgs = await _context.Organizacion.ToListAsync();
            foreach (var org in listorgs)
            {
                if (dispositivo.OrganizacionId == org.Id) {
                    _context.Dispositivo.Add(dispositivo);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetDispositivo", new { id = dispositivo.Id }, dispositivo);
                }

            }
            return BadRequest("Esta organización no existe");
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivo(int id)
        {
            if (_context.Dispositivo == null)
            {
                return NotFound();
            }
            var dispositivo = await _context.Dispositivo.FindAsync(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            _context.Dispositivo.Remove(dispositivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllDispositivos()
        {
            if (_context.Dispositivo == null)
            {
                return NotFound();
            }

            List<Dispositivo> listdispositivos = await _context.Dispositivo.ToListAsync();

            foreach (var disp in listdispositivos)
            {
                _context.Dispositivo.Remove(disp);
            }
            await _context.SaveChangesAsync();

            return Ok("Todos los dispositivos eliminadas correctamente");
        }
        private bool DispositivoExists(int id)
        {
            return (_context.Dispositivo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
