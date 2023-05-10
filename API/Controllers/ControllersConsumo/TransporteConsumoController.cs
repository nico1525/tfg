using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Consumos;
using API.Models.Context;
using API.Calculos;
using API.Models;
using AutoMapper;
using API.Authorization;

namespace API.Controllers.ControllersConsumo
{
    [Authorize]
    [Route("api/Organizacion/Transporte/Consumo")]
    [ApiController]
    public class TransporteConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TransporteConsumoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransporteConsumoDTO>>> GetTransporteConsumo()
        {
            //Devuelve todos los consumos de transporte de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.TransporteConsumo.ToListAsync();
            List<TransporteConsumoDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                var transporte = await _context.Transporte.FindAsync(item.TransporteId);

                if (transporte.OrganizacionId == currentUser.OrganizacionId)
                {
                    TransporteConsumoDTO transporteConsumo = _mapper.Map<TransporteConsumoDTO>(item);
                    orgConsumoList.Add(transporteConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("{transporteid}")]
        public async Task<ActionResult<IEnumerable<TransporteConsumoDTO>>> GetTransporteConsumo(int id)
        {
            //Devuelve todos los consumos de un transporte
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var transporte = await _context.Transporte.FindAsync(id);
                var listconsumos = await _context.TransporteConsumo.ToListAsync();

                if (currentUser.OrganizacionId != transporte.OrganizacionId)
                {
                    return BadRequest("Este transporte no pertenece a esta organización");
                }
                List<TransporteConsumoDTO> orgConsumoList = new();

                foreach (var item in listconsumos)
                {
                    var transporteref = await _context.Transporte.FindAsync(item.TransporteId);
                    if (transporteref.Id == id)
                    {
                        TransporteConsumoDTO transporteConsumo = _mapper.Map<TransporteConsumoDTO>(item);
                        orgConsumoList.Add(transporteConsumo);
                    }
                }
                return orgConsumoList;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún transporte");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporteConsumo(int id, TransporteConsumoModifyDTO transporteConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var transporteChange = await _context.TransporteConsumo.FindAsync(id);

                var transporte = await _context.Transporte.FindAsync(transporteChange.TransporteId);
                DateTime test = new DateTime(1, 1, 1, 0, 0, 0, 0);
                if (currentUser.OrganizacionId != transporte.OrganizacionId)
                {
                    return BadRequest("Este consumo de transporte no pertenece a esta organización");
                }
                if (transporteConsumo.Edificio != null) transporteChange.Edificio = transporteConsumo.Edificio;
                if (transporteConsumo.CantidadCombustible > 0) transporteChange.CantidadCombustible = transporteConsumo.CantidadCombustible;
                if (transporteConsumo.FechaInicio != test) transporteChange.FechaInicio = transporteConsumo.FechaInicio;
                if (transporteConsumo.FechaFin != test) transporteChange.FechaFin = transporteConsumo.FechaFin;

                if (DateTime.Compare(transporteChange.FechaInicio, transporteChange.FechaFin) > 0)
                {
                    return BadRequest("La fecha final debe ser superior a la fecha inicial");
                }
                transporteChange.Consumo = CalculoTransporte.CalculoConsumoTransporte(transporte, transporteChange, _context);

                await _context.SaveChangesAsync();

                return Ok("Consumo del Transporte con Id: " + transporte.Id + " modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de transporte");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransporteConsumo>> PostTransporteConsumo(TransporteConsumoCreateDTO transporteConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(transporteConsumoDTO.FechaInicio, transporteConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            List<Transporte> listtransportes = await _context.Transporte.ToListAsync();
            foreach (var transporte in listtransportes)
            {
                if (transporteConsumoDTO.TransporteId == transporte.Id && transporte.OrganizacionId == currentUser.OrganizacionId)
                {
                    TransporteConsumo transporteConsumo = _mapper.Map<TransporteConsumo>(transporteConsumoDTO);

                    transporteConsumo.Consumo = CalculoTransporte.CalculoConsumoTransporte(transporte, transporteConsumo, _context);
                    transporteConsumo.TransporteRef = transporte;
                    transporteConsumo.TransporteId = transporte.Id;

                    _context.TransporteConsumo.Add(transporteConsumo);
                    await _context.SaveChangesAsync();
                    return Ok("Consumo Transporte creado correctamente");
                }

            }
            return BadRequest("No existe un transporte con este id registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporteConsumo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var transporteDelete = await _context.TransporteConsumo.FindAsync(id);
                if (currentUser.OrganizacionId != transporteDelete.TransporteRef.OrganizacionId)
                {
                    return BadRequest("Este consumo de transporte no existe o no pertenece a esta organización");
                }
                _context.TransporteConsumo.Remove(transporteDelete);
                await _context.SaveChangesAsync();

                return Ok("Consumo del transporte eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de transporte");
            }
        }
    }
}
