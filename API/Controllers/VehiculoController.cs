using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;
using API.Helpers;
using API.Authorization;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculo()
        {
          if (_context.Vehiculo == null)
          {
              return NotFound();
          }
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<Vehiculo> listatodosvehiculos = await _context.Vehiculo.ToListAsync();
          List<Vehiculo> listavehiculosorg = new();

        foreach (var vehiculo in listatodosvehiculos)
        {
            if(vehiculo.OrganizacionId == currentUser.Id)
            {
                listavehiculosorg.Add(vehiculo);
            }
        }

        if (currentUser.Role != Role.SuperAdmin) { return listavehiculosorg; } 
        else{ return listatodosvehiculos; }


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
            var currentOrg = (Organizacion)HttpContext.Items["Organizacion"];
            vehiculo.OrganizacionId = currentOrg.Id;
            vehiculo.OrganizacionRef = currentOrg;
            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculo creado correctamente");
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