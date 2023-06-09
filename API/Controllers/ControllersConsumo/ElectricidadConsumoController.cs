using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Consumos;
using API.Models.Context;
using API.Authorization;
using API.Calculos;
using API.Models;
using AutoMapper;

namespace API.Controllers.ControllersConsumo
{
    [Authorize]
    [Route("api/Organizacion/Electricidad/Consumo")]
    [ApiController]
    public class ElectricidadConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ElectricidadConsumoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricidadConsumoDTO>>> GetElectricidadConsumo()
        {
            //Devuelve todos los consumos de electricidad de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.ElectricidadConsumo.ToListAsync();
            List<ElectricidadConsumoDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                var electricidad = await _context.Electricidad.FindAsync(item.ElectricidadId);

                if (electricidad.OrganizacionId == currentUser.OrganizacionId)
                {
                    ElectricidadConsumoDTO electricidadConsumo = _mapper.Map<ElectricidadConsumoDTO>(item);
                    orgConsumoList.Add(electricidadConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ElectricidadConsumoDTO>> GetElectricidadConsumoId(int id)
        {
            //Devuelve el consumos por su id
            var consumo = await _context.ElectricidadConsumo.FindAsync(id);
            ElectricidadConsumoDTO consumodto = _mapper.Map<ElectricidadConsumoDTO>(consumo);

            return consumodto;
        }

        [HttpGet("{electricidadid}")]
        public async Task<ActionResult<IEnumerable<ElectricidadConsumoDTO>>> GetElectricidadConsumo(int id)
        {
            //Devuelve todos los consumos de un electricidad
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var electricidad = await _context.Electricidad.FindAsync(id);
                var listconsumos = await _context.ElectricidadConsumo.ToListAsync();

                if (currentUser.OrganizacionId != electricidad.OrganizacionId)
                {
                    return BadRequest("Esta dispositivo no pertenece a esta organización");
                }
                List<ElectricidadConsumoDTO> orgConsumoList = new();

                foreach (var item in listconsumos)
                {
                    var electricidadref = await _context.Electricidad.FindAsync(item.ElectricidadId);
                    if (electricidadref.Id == id)
                    {
                        ElectricidadConsumoDTO electricidadConsumo = _mapper.Map<ElectricidadConsumoDTO>(item);
                        orgConsumoList.Add(electricidadConsumo);
                    }
                }
                return orgConsumoList;
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún dispositivo");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricidadConsumo(int id, ElectricidadConsumoModifyDTO electricidadConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var electricidadChange = await _context.ElectricidadConsumo.FindAsync(id);

                var electricidad = await _context.Electricidad.FindAsync(electricidadChange.ElectricidadId);
                DateTime test = new(1, 1, 1, 0, 0, 0, 0);
                if (currentUser.OrganizacionId != electricidad.OrganizacionId)
                {
                    return BadRequest("Este consumo de electricidad de dispositivo no pertenece a esta organización");
                }
                if (electricidadConsumo.Edificio != null && electricidadConsumo.Edificio != "") electricidadChange.Edificio = electricidadConsumo.Edificio;
                if (electricidadConsumo.ComercializadoraId > 0) electricidadChange.ComercializadoraId = electricidadConsumo.ComercializadoraId;
                if (electricidadConsumo.Kwh > 0) electricidadChange.Kwh = electricidadConsumo.Kwh;
                if (electricidadConsumo.FechaInicio != test) electricidadChange.FechaInicio = electricidadConsumo.FechaInicio;
                if (electricidadConsumo.FechaFin != test) electricidadChange.FechaFin = electricidadConsumo.FechaFin;

                if (DateTime.Compare(electricidadChange.FechaInicio, electricidadChange.FechaFin) > 0)
                {
                    return BadRequest("La fecha final debe ser superior a la fecha inicial");
                }

                try
                {
                    electricidadChange.Consumo = CalculoElectricidad.CalculoConsumoElectricidad(electricidadChange, _context);
                    electricidadChange.Comercializadora = CalculoElectricidad.GetComercializadora(electricidadChange, _context);
                }
                catch (NullReferenceException)
                {
                    return BadRequest("Este Id de Comercializadora no es válido");
                }

                await _context.SaveChangesAsync();

                return Ok("Consumo de electricidad del dispositivocon Id: " + electricidad.Id + " modificado correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún consumo de electricidad");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ElectricidadConsumo>> PostElectricidadConsumo(ElectricidadConsumoCreateDTO electricidadConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(electricidadConsumoDTO.FechaInicio, electricidadConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            Electricidad electricidad = await _context.Electricidad.FindAsync(electricidadConsumoDTO.ElectricidadId);
           
            if (electricidadConsumoDTO.ElectricidadId == electricidad.Id && electricidad.OrganizacionId == currentUser.OrganizacionId)
            {
                ElectricidadConsumo electricidadConsumo = _mapper.Map<ElectricidadConsumo>(electricidadConsumoDTO);
                try
                {
                    electricidadConsumo.Consumo = CalculoElectricidad.CalculoConsumoElectricidad(electricidadConsumo, _context);
                    electricidadConsumo.Comercializadora = CalculoElectricidad.GetComercializadora(electricidadConsumo, _context);
                }
                catch (NullReferenceException)
                {
                    return BadRequest("Este Id de Comercializadora no es válido");
                }
                electricidadConsumo.ElectricidadId = electricidad.Id;

                _context.ElectricidadConsumo.Add(electricidadConsumo);
                await _context.SaveChangesAsync();
                return Ok("Consumo de electricidad para dispositivo con Id: " + electricidad.Id + " creado correctamente");
            }
            
            return BadRequest("No existe un dispositivo con este id registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricidadConsumo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var electricidadDelete = await _context.ElectricidadConsumo.FindAsync(id);
                var Electricidad = await _context.Electricidad.FindAsync(electricidadDelete.ElectricidadId);

                if (currentUser.OrganizacionId != Electricidad.OrganizacionId)
                {
                    return BadRequest("Este consumo de electricidad no existe o no pertenece a esta organización");
                }
                _context.ElectricidadConsumo.Remove(electricidadDelete);
                await _context.SaveChangesAsync();

                return Ok("Consumo de electricidad eliminado correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún consumo de electricidad");
            }
        }
    }
}
