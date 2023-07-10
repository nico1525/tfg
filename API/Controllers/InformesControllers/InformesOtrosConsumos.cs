using API.Models.Query_Models;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Authorization;
using API.Models.Context;
using AutoMapper;
using MySqlX.XDevAPI.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using System.Net;
using System.Diagnostics.Metrics;
using static API.Controllers.InformesControllers.InformesOtrosConsumosController;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/OtrosConsumos")]
    [ApiController]
    public class InformesOtrosConsumosController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesOtrosConsumosController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoOtrosConsumosId>> AllOtrosConsumosFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los vehiculos entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoOtrosConsumosId query = new();
            try
            {
                query = (from c in _context.OtrosConsumos
                         where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && c.OrganizacionId == currentUser.OrganizacionId
                         group c by c.Id into g
                         select new ConsumoOtrosConsumosId()
                         {
                             Total_consumido = g.Sum(r => r.Consumo),
                             Total_cantidad_consumida = g.Sum(r => r.CantidadConsumo)
                         }).Single();
            }
            catch (Exception e)
            {
                return BadRequest("No existen consumos para este dispositivo entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoOtrosConsumosId>> OtrosConsumossFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 vehiculo entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoOtrosConsumosId query = new();

            try
            {
                var consumo = await _context.OtrosConsumos.FindAsync(id);
                if (consumo == null || currentUser.OrganizacionId != consumo.OrganizacionId)
                {
                    return BadRequest("Este consumo de otras fuentes no existe o no pertenece a esta organización");
                }
                query = (from c in _context.OtrosConsumos
                         where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && c.Id == id
                         group c by c.Id into g
                         select new ConsumoOtrosConsumosId()
                         {
                             Total_consumido = g.Sum(r => r.Consumo),
                             Total_cantidad_consumida = g.Sum(r => r.CantidadConsumo)
                         }).Single();
            }
            catch (Exception e)
            {
                return BadRequest($"No existen consumos de otras fuentes con id: {id} entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}/mes")]
        public async Task<ActionResult<List<ConsumoMesOtrosConsumos>>> OtrosConsumossFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var consumo = await _context.OtrosConsumos.FindAsync(id);
            if (consumo == null || currentUser.OrganizacionId != consumo.OrganizacionId)
            {
                return BadRequest("Este consumo de otras fuentes no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 vehiculo entre dos fechas agrupado por meses
            List<ConsumoMesOtrosConsumos> query = (from c in _context.OtrosConsumos
                                                   where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && c.Id == id
                                                   group c by c.FechaInicio.Month into g
                                                   orderby g.Key
                                                   select new ConsumoMesOtrosConsumos()
                                                   {
                                                       Consumo_mes = g.Sum(r => r.Consumo),
                                                       Cantidad_consumida_mes = g.Sum(r => r.CantidadConsumo)
                                                   }).ToList();
            return query;

        }
    }
}
