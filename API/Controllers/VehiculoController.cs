using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;
using NuGet.DependencyResolver;

namespace API.Controllers
{
    [Route("api/Organizacion/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VehiculoController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculo()
        {
          if (_context.Vehiculo == null)
          {
              return NotFound();
          }
            return await _context.Vehiculo.ToListAsync();
        }

        [HttpGet("{org}")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculo(int org)
        {
          if (_context.Vehiculo == null)
          {
              return NotFound();
          }
          List<Vehiculo> listavehiculos = await _context.Vehiculo.ToListAsync();
          List<Vehiculo> listavehiculosorg = new();

            foreach (var vehiculo in listavehiculos)
            {
                if(vehiculo.OrganizacionId == org)
                {
                    listavehiculosorg.Add(vehiculo);
                }
            }

            return listavehiculosorg;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
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
        public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        {
          if (_context.Vehiculo == null)
          {
              return Problem("Entity set 'DatabaseContext.Vehiculo'  is null.");
          }
            List<Organizacion> listorgs = await _context.Organizacion.ToListAsync();
            foreach (var org in listorgs)
            {
                if (vehiculo.OrganizacionId == org.Id)
                {
                    vehiculo.OrganizacionRef = org;
                    _context.Vehiculo.Add(vehiculo);
                    await _context.SaveChangesAsync();
                    return Ok("Vehiculo creado correctamente");
                }

            }
            return BadRequest("No puedes agregar un vehiculo a una organizacion que no existe");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            if (_context.Vehiculo == null)
            {
                return NotFound();
            }
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllVehiculos()
        {
            if (_context.Vehiculo == null)
            {
                return NotFound();
            }

            List<Vehiculo> listvehiculos = await _context.Vehiculo.ToListAsync();

            foreach (var disp in listvehiculos)
            {
                _context.Vehiculo.Remove(disp);
            }
            await _context.SaveChangesAsync();

            return Ok("Todos los vehiculos eliminados correctamente");
        }
        private bool VehiculoExists(int id)
        {
            return (_context.Vehiculo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
