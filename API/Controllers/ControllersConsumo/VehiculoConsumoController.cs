using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Consumos;
using API.Models.Context;
using API.Models;
using API.Calculos;
using API.Authorization;
using AutoMapper;

namespace API.Controllers.ControllersConsumo
{
    [Authorize]
    [Route("api/Organizacion/Vehiculo/Consumo")]
    [ApiController]
    public class VehiculoConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public VehiculoConsumoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoConsumoDTO>>> GetVehiculoConsumo()
        {
            //Devuelve todos los consumos de vehiculo de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.VehiculoConsumo.ToListAsync();
            List<VehiculoConsumoDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                var vehiculo = await _context.Vehiculo.FindAsync(item.VehiculoId);

                if (vehiculo.OrganizacionId == currentUser.OrganizacionId)
                {
                    VehiculoConsumoDTO vehiculoConsumo = _mapper.Map<VehiculoConsumoDTO>(item);
                    orgConsumoList.Add(vehiculoConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("{vehiculoid}")]
        public async Task<ActionResult<IEnumerable<VehiculoConsumoDTO>>> GetVehiculoConsumo(int id)
        {
            //Devuelve todos los consumos de un vehiculo
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try { 
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            var listconsumos = await _context.VehiculoConsumo.ToListAsync();

            if (currentUser.OrganizacionId != vehiculo.OrganizacionId)
            {
                return BadRequest("Este vehiculo no pertenece a esta organización");
            }
            List<VehiculoConsumoDTO> orgConsumoList = new();
            
            foreach (var item in listconsumos)
            {
                var vehiculoref = await _context.Vehiculo.FindAsync(item.VehiculoId);
                if (vehiculoref.Id == id)
                {
                    VehiculoConsumoDTO vehiculoConsumo = _mapper.Map<VehiculoConsumoDTO>(item);
                    orgConsumoList.Add(vehiculoConsumo);
                }
            }
            return orgConsumoList;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún vehiculo");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculoConsumo(int id, VehiculoConsumoModifyDTO vehiculoConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var vehiculoChange = await _context.VehiculoConsumo.FindAsync(id);
            
            var vehiculo = await _context.Vehiculo.FindAsync(vehiculoChange.VehiculoId);
            DateTime test = new DateTime(1,1,1,0,0,0,0);
            if (currentUser.OrganizacionId != vehiculo.OrganizacionId)
            {
                return BadRequest("Este consumo de vehiculo no pertenece a esta organización");
            }
            if (vehiculoConsumo.Edificio != null) vehiculoChange.Edificio = vehiculoConsumo.Edificio;
            if (vehiculoConsumo.TipoCombustible != null) vehiculoChange.TipoCombustible = vehiculoConsumo.TipoCombustible;
            if (vehiculoConsumo.CantidadCombustible > 0) vehiculoChange.CantidadCombustible = vehiculoConsumo.CantidadCombustible;
            if (vehiculoConsumo.FechaInicio != test) vehiculoChange.FechaInicio = vehiculoConsumo.FechaInicio;
            if (vehiculoConsumo.FechaFin != test) vehiculoChange.FechaFin = vehiculoConsumo.FechaFin;

            if (DateTime.Compare(vehiculoChange.FechaInicio, vehiculoChange.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            vehiculoChange.Consumo = CalculoVehiculo.CalculoConsumoVehiculo(vehiculo, vehiculoChange, _context);

            await _context.SaveChangesAsync();

            return Ok("Consumo del Vehículo con Id: " + vehiculo.Id + " modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de vehiculo");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehiculoConsumo>> PostVehiculoConsumo(VehiculoConsumoCreateDTO vehiculoConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(vehiculoConsumoDTO.FechaInicio, vehiculoConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            List<Vehiculo> listvehiculos = await _context.Vehiculo.ToListAsync();
            foreach (var vehiculo in listvehiculos)
            {
                if (vehiculoConsumoDTO.VehiculoId == vehiculo.Id && vehiculo.OrganizacionId == currentUser.OrganizacionId)
                {
                    string? combustible = vehiculoConsumoDTO.TipoCombustible;
                    string? categoria = vehiculo.CategoriaVehiculo;
                    switch (categoria) {
                        case "N1": if(combustible.Equals("LPG") || combustible.Equals("CNG")) { 
                                return BadRequest("Los combustibles LPG y CNG no están permitidos para los vehículos de categoría N1"); }
                            break;
                        case "N2":
                        case "N3":
                        case "M2":
                        case "M3":
                            if (combustible.Equals("LPG") || combustible.Equals("CNG"))
                            {
                                return BadRequest("El combustible LPG no está permitido para los vehículos de categoría N2, N3, M2 o M3");
                            }
                            break;
                        case "L":
                            if(!combustible.Equals("E5") && !combustible.Equals("E10") && !combustible.Equals("E85") && !combustible.Equals("E100"))
                            {
                                return BadRequest("Para los vehículos de categoría L solo están permitidos los combustibles E5, E10, E85 y E100");
                            }
                            break;
                    }

                    VehiculoConsumo vehiculoConsumo = _mapper.Map<VehiculoConsumo>(vehiculoConsumoDTO);

                    vehiculoConsumo.Consumo = CalculoVehiculo.CalculoConsumoVehiculo(vehiculo, vehiculoConsumo, _context);
                    vehiculoConsumo.VehiculoRef = vehiculo;
                    vehiculoConsumo.VehiculoId = vehiculo.Id;

                    _context.VehiculoConsumo.Add(vehiculoConsumo);
                    await _context.SaveChangesAsync();
                    return Ok("Consumo Vehiculo creado correctamente");
                }

            }
            return BadRequest("No existe un vehículo con este id registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculoConsumo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try { 
            var vehiculoDelete = await _context.VehiculoConsumo.FindAsync(id);
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (currentUser.OrganizacionId != vehiculo.OrganizacionId)
            {
                return BadRequest("Este consumo de vehiculo no existe o no pertenece a esta organización");
            }
            _context.VehiculoConsumo.Remove(vehiculoDelete);
            await _context.SaveChangesAsync();

            return Ok("Consumo del vehículo eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de vehiculo");
            }
        }

    }
}
