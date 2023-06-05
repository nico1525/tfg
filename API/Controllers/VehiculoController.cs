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
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    [Route("api/Organizacion/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public VehiculoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetVehiculo()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<Vehiculo> listatodosvehiculos = await _context.Vehiculo.ToListAsync();
            List<VehiculoDTO> listavehiculosorg = new();

            foreach (var vehiculo in listatodosvehiculos)
            {
                if(vehiculo.OrganizacionId == currentUser.OrganizacionId)
                {
                    VehiculoDTO vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
                    listavehiculosorg.Add(vehiculoDTO);
                }
            }

            if (currentUser.Role != Role.SuperAdmin) { return listavehiculosorg; } 
            else{ return listatodosvehiculos; }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoDTO>> GetVehiculoById(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Vehiculo vehiculo = await _context.Vehiculo.FindAsync(id);
            VehiculoDTO vehiculoDTO = new();
            if (vehiculo.OrganizacionId == currentUser.OrganizacionId)
            {
                vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
            }
            else {
                return BadRequest("Este vehículo no existe o no pertenece a esta organización");
            }
            return vehiculoDTO;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, VehiculoModifyDTO vehiculo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try {
            var vehiculoChange = await _context.Vehiculo.FindAsync(id);
            if (currentUser.OrganizacionId != vehiculoChange.OrganizacionId)
            {
                return BadRequest("Este vehículo no existe o no pertenece a esta organización");
            }
            if (_context.Vehiculo.Any(e => e.Matricula == vehiculo.Matricula && e.Id != vehiculoChange.Id))
            {
                return BadRequest("Ya existe un usuario con ese email");
            }
            else
            {
                if (vehiculo.Matricula != null && vehiculo.Matricula != null) vehiculoChange.Matricula = vehiculo.Matricula;
            }
            if (vehiculo.Edificio != null && vehiculo.Edificio != null) vehiculoChange.Edificio = vehiculo.Edificio;
            await _context.SaveChangesAsync();

            return Ok("Vehículo modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún vehiculo");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(VehiculoCreateDTO vehiculoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (_context.Vehiculo.Any(e => e.Matricula == vehiculoDTO.Matricula))
            {
                return BadRequest("Ya existe un vehículo con esa matrícula");
            }
            Vehiculo vehiculo = _mapper.Map<Vehiculo>(vehiculoDTO);

            vehiculo.OrganizacionId = currentUser.OrganizacionId;
            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculo creado correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try { 
            var vehiculoDelete = await _context.Vehiculo.FindAsync(id);
            if (currentUser.OrganizacionId != vehiculoDelete.OrganizacionId)
            {
                return BadRequest("Este vehiculo no existe o no pertenece a esta organización");
            }
            _context.Vehiculo.Remove(vehiculoDelete);
            await _context.SaveChangesAsync();

            return Ok("Vehículo eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún vehiculo");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllVehiculos()
        {
            List<Vehiculo> vehiculolist = await _context.Vehiculo.ToListAsync();

            foreach (var vehiculo in vehiculolist)
            {
                _context.Vehiculo.Remove(vehiculo);
            }
            await _context.SaveChangesAsync();

            return Ok("Todos los vehiculos eliminados correctamente");
        }
    }
}