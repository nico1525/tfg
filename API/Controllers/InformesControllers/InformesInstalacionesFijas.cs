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
using static API.Controllers.InformesControllers.InformesInstalacionesFijasController;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/InstalacionesFijas")]
    [ApiController]
    public class InformesInstalacionesFijasController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesInstalacionesFijasController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoCombustibleId>> AllInstalacionesFijasFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los InstalacionesFijass entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoCombustibleId query = new();
            try
            {
                query = (from c in _context.InstalacionesFijasConsumo
                                           join v in _context.InstalacionesFijas
                                           on c.InstalacionesFijasId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.OrganizacionId == currentUser.OrganizacionId
                                           group c by v.OrganizacionId into g
                                           select new ConsumoCombustibleId()
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
        public async Task<ActionResult<ConsumoCombustibleId>> InstalacionesFijassFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 InstalacionesFijas entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoCombustibleId query = new();

            try
            { 
                var InstalacionesFijas = await _context.InstalacionesFijas.FindAsync(id);
                if (currentUser.OrganizacionId != InstalacionesFijas.OrganizacionId)
                {
                    return BadRequest("Esta Instalación Fija no existe o no pertenece a esta organización");
                }
                query = (from c in _context.InstalacionesFijasConsumo
                                           join v in _context.InstalacionesFijas
                                           on c.InstalacionesFijasId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                           group c by c.InstalacionesFijasId into g
                                           select new ConsumoCombustibleId()
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
        public async Task<ActionResult<List<ConsumoMes>>> InstalacionesFijassFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var InstalacionesFijas = await _context.InstalacionesFijas.FindAsync(id);
            if (currentUser.OrganizacionId != InstalacionesFijas.OrganizacionId)
            {
                return BadRequest("Esta Instalación Fija no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 InstalacionesFijas entre dos fechas agrupado por meses
            List<ConsumoMes> query = (from c in _context.InstalacionesFijasConsumo
                                      join v in _context.InstalacionesFijas
                                      on c.InstalacionesFijasId equals v.Id
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
