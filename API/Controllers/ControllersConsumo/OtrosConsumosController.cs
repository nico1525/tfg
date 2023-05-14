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
    [Route("api/Organizacion/[controller]")]
    [ApiController]
    public class OtrosConsumosController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OtrosConsumosController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtrosConsumosDTO>>> GetOtrosConsumos()
        {
            //Devuelve todos los consumos de otras fuentes de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.OtrosConsumos.ToListAsync();
            List<OtrosConsumosDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                if (item.OrganizacionId == currentUser.OrganizacionId)
                {
                    OtrosConsumosDTO otrosconsumosConsumo = _mapper.Map<OtrosConsumosDTO>(item);
                    orgConsumoList.Add(otrosconsumosConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("{otrosconsumosid}")]
        public async Task<ActionResult<OtrosConsumosDTO>> GetOtrosConsumosById(int id)
        {
            
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var otrosconsumos = await _context.OtrosConsumos.FindAsync(id);
                var listconsumos = await _context.OtrosConsumos.ToListAsync();

                if (currentUser.OrganizacionId != otrosconsumos.OrganizacionId)
                {
                    return BadRequest("Esta instalación fija no pertenece a esta organización");
                }
                OtrosConsumosDTO otrosconsumosConsumo = _mapper.Map<OtrosConsumosDTO>(otrosconsumos);
                    
                return otrosconsumosConsumo;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de otras fuentes");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtrosConsumos(int id, OtrosConsumosModifyDTO otrosconsumosConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var otrosconsumosChange = await _context.OtrosConsumos.FindAsync(id);

                DateTime test = new DateTime(1, 1, 1, 0, 0, 0, 0);
                if (currentUser.OrganizacionId != otrosconsumosChange.OrganizacionId)
                {
                    return BadRequest("Este consumo de instalación fija no pertenece a esta organización");
                }
                if (otrosconsumosConsumo.Edificio != null) otrosconsumosChange.Edificio = otrosconsumosConsumo.Edificio;
                if (otrosconsumosConsumo.Nombre != null) otrosconsumosChange.Nombre = otrosconsumosConsumo.Nombre;
                if (otrosconsumosConsumo.CategoriaActividad != null) otrosconsumosChange.CategoriaActividad = otrosconsumosConsumo.CategoriaActividad;
                if (otrosconsumosConsumo.CantidadConsumo > 0) otrosconsumosChange.CantidadConsumo = otrosconsumosConsumo.CantidadConsumo;
                if (otrosconsumosConsumo.FactorEmision > 0) otrosconsumosChange.FactorEmision = otrosconsumosConsumo.FactorEmision;
                if (otrosconsumosConsumo.FechaInicio != test) otrosconsumosChange.FechaInicio = otrosconsumosConsumo.FechaInicio;
                if (otrosconsumosConsumo.FechaFin != test) otrosconsumosChange.FechaFin = otrosconsumosConsumo.FechaFin;

                if (DateTime.Compare(otrosconsumosChange.FechaInicio, otrosconsumosChange.FechaFin) > 0)
                {
                    return BadRequest("La fecha final debe ser superior a la fecha inicial");
                }

                try
                {
                    otrosconsumosChange.Consumo = otrosconsumosChange.FactorEmision * otrosconsumosChange.CantidadConsumo;
                }
                catch (Exception ex)
                {
                    return BadRequest("Algo ha ido mal en el calculo");
                }
                await _context.SaveChangesAsync();

                return Ok("Consumo de emisiones de otras fuentes con Id: " + id + " modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de emisiones de otras fuentes");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OtrosConsumos>> PostOtrosConsumos(OtrosConsumosCreateDTO otrosconsumosConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(otrosconsumosConsumoDTO.FechaInicio, otrosconsumosConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            OtrosConsumos otrosconsumosConsumo = _mapper.Map<OtrosConsumos>(otrosconsumosConsumoDTO);
            try
            {
                otrosconsumosConsumo.Consumo = otrosconsumosConsumoDTO.FactorEmision * otrosconsumosConsumoDTO.CantidadConsumo;
            }
            catch (Exception ex)
            {
                return BadRequest("Algo ha ido mla en el calculo del consumo");
            }
            otrosconsumosConsumo.OrganizacionId = currentUser.OrganizacionId;
            otrosconsumosConsumo.OrganizacionRef = currentUser.OrganizacionRef;

            _context.OtrosConsumos.Add(otrosconsumosConsumo);
            await _context.SaveChangesAsync();
            return Ok("Consumo de Emisiones de otras fuentes creado correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtrosConsumos(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var otrosconsumosDelete = await _context.OtrosConsumos.FindAsync(id);
                if (currentUser.OrganizacionId != otrosconsumosDelete.OrganizacionId)
                {
                    return BadRequest("Este consumo de otras fuentes no existe o no pertenece a esta organización");
                }
                _context.OtrosConsumos.Remove(otrosconsumosDelete);
                await _context.SaveChangesAsync();

                return Ok("Consumo de otras fuentes eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de instalación fija");
            }
        }
    }
}
