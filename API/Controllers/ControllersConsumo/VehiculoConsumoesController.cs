using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Consumos;
using API.Models.Context;
using API.Models;
using API.Calculos;

namespace API.Controllers.ControllersConsumo
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoConsumoesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VehiculoConsumoesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoConsumo>>> GetVehiculoConsumo()
        {
          if (_context.VehiculoConsumo == null)
          {
              return NotFound();
          }
            return await _context.VehiculoConsumo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoConsumo>> GetVehiculoConsumo(int id)
        {
          if (_context.VehiculoConsumo == null)
          {
              return NotFound();
          }
            var vehiculoConsumo = await _context.VehiculoConsumo.FindAsync(id);

            if (vehiculoConsumo == null)
            {
                return NotFound();
            }

            return vehiculoConsumo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculoConsumo(int id, VehiculoConsumo vehiculoConsumo)
        {
            if (id != vehiculoConsumo.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehiculoConsumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoConsumoExists(id))
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
        public async Task<ActionResult<VehiculoConsumo>> PostVehiculoConsumo(VehiculoConsumo vehiculoConsumo)
        {
          if (_context.VehiculoConsumo == null)
          {
              return Problem("Entity set 'DatabaseContext.VehiculoConsumo'  is null.");
          }
            List<Vehiculo> listvehiculos = await _context.Vehiculo.ToListAsync();
            foreach (var vehiculo in listvehiculos)
            {
                if (vehiculoConsumo.VehiculoId == vehiculo.Id)
                {
                    vehiculoConsumo.Consumo = Calculo.CalculoConsumoVehiculo(vehiculo, vehiculoConsumo, _context);

                    _context.VehiculoConsumo.Add(vehiculoConsumo);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetDispositivo", new { id = vehiculoConsumo.Id }, vehiculoConsumo);
                }

            }
            return BadRequest("No puedes agregar un consumo a un vehiculo que no esta registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculoConsumo(int id)
        {
            if (_context.VehiculoConsumo == null)
            {
                return NotFound();
            }
            var vehiculoConsumo = await _context.VehiculoConsumo.FindAsync(id);
            if (vehiculoConsumo == null)
            {
                return NotFound();
            }

            _context.VehiculoConsumo.Remove(vehiculoConsumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculoConsumoExists(int id)
        {
            return (_context.VehiculoConsumo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
