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
using static API.Controllers.InformesControllers.InformesTransporteController;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/Transporte")]
    [ApiController]
    public class InformesTransporteController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesTransporteController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoTransporteId>> AllTransporteFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los Transportes entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoTransporteId query = new();
            try
            {
                query = (from c in _context.TransporteConsumo
                                           join v in _context.Transporte
                                           on c.TransporteId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.OrganizacionId == currentUser.OrganizacionId
                                           group c by v.OrganizacionId into g
                                           select new ConsumoTransporteId()
                                           {
                                               Total_consumido = g.Sum(r => r.Consumo),
                                               Total_combustible = g.Sum(r => r.CantidadCombustible)
                                           }).Single();
            } catch(Exception e)
            {
                return BadRequest("No existen consumos para este dispositivo entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoTransporteId>> TransportesFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 Transporte entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoTransporteId query = new();

            try
            { 
                var Transporte = await _context.Transporte.FindAsync(id);
                if (currentUser.OrganizacionId != Transporte.OrganizacionId)
                {
                    return BadRequest("Este Transporte no existe o no pertenece a esta organización");
                }
                query = (from c in _context.TransporteConsumo
                                           join v in _context.Transporte
                                           on c.TransporteId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                           group c by c.TransporteId into g
                                           select new ConsumoTransporteId()
                                           {
                                               Total_consumido = g.Sum(r => r.Consumo),
                                               Total_combustible = g.Sum(r => r.CantidadCombustible),
                                           }).Single();
            } 
            catch(Exception e)
            {
                return BadRequest($"No existen consumos para el dispositivo con id: {id} entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}/mes")]
        public async Task<ActionResult<List<ConsumoMes>>> TransportesFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var Transporte = await _context.Transporte.FindAsync(id);
            if (currentUser.OrganizacionId != Transporte.OrganizacionId)
            {
                return BadRequest("Este Transporte no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 Transporte entre dos fechas agrupado por meses
            List<ConsumoMes> query = (from c in _context.TransporteConsumo
                                      join v in _context.Transporte
                                      on c.TransporteId equals v.Id
                                      where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                      group c by c.FechaInicio.Month into g
                                      orderby g.Key
                                      select new ConsumoMes()
                                      {
                                          Consumo_mes = g.Sum(r => r.Consumo),
                                          Combustible_mes = g.Sum(r => r.CantidadCombustible),
                                          Mes = g.Key
                                      }).ToList();
            return query;
        }

    }
}
