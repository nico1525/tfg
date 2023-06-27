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
using static API.Controllers.InformesControllers.InformesMaquinariaController;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/Maquinaria")]
    [ApiController]
    public class InformesMaquinariaController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesMaquinariaController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoMaquinariaId>> AllMaquinariaFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los Maquinarias entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoMaquinariaId query = new();
            try
            {
                query = (from c in _context.MaquinariaConsumo
                                           join v in _context.Maquinaria
                                           on c.MaquinariaId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.OrganizacionId == currentUser.OrganizacionId
                                           group c by v.OrganizacionId into g
                                           select new ConsumoMaquinariaId()
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
        public async Task<ActionResult<ConsumoMaquinariaId>> MaquinariasFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 Maquinaria entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoMaquinariaId query = new();

            try
            { 
                var Maquinaria = await _context.Maquinaria.FindAsync(id);
                if (currentUser.OrganizacionId != Maquinaria.OrganizacionId)
                {
                    return BadRequest("Este Maquinaria no existe o no pertenece a esta organización");
                }
                query = (from c in _context.MaquinariaConsumo
                                           join v in _context.Maquinaria
                                           on c.MaquinariaId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                           group c by c.MaquinariaId into g
                                           select new ConsumoMaquinariaId()
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
        public async Task<ActionResult<List<ConsumoMes>>> MaquinariasFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var Maquinaria = await _context.Maquinaria.FindAsync(id);
            if (currentUser.OrganizacionId != Maquinaria.OrganizacionId)
            {
                return BadRequest("Este Maquinaria no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 Maquinaria entre dos fechas agrupado por meses
            List<ConsumoMes> query = (from c in _context.MaquinariaConsumo
                                      join v in _context.Maquinaria
                                      on c.MaquinariaId equals v.Id
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
