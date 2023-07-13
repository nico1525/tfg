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
using static API.Controllers.InformesControllers.InformesElectricidadController;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/Electricidad")]
    [ApiController]
    public class InformesElectricidadController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesElectricidadController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoElectricidadId>> AllElectricidadFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los Electricidads entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoElectricidadId query = new();
            try
            {
                query = (from c in _context.ElectricidadConsumo
                                           join v in _context.Electricidad
                                           on c.ElectricidadId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.OrganizacionId == currentUser.OrganizacionId
                                           group c by v.OrganizacionId into g
                                           select new ConsumoElectricidadId()
                                           {
                                               Total_consumido = g.Sum(r => r.Consumo),
                                               Total_Kilovatios = g.Sum(r => r.Kwh),
                                           }).Single();
            } catch(Exception e)
            {
                return BadRequest("No existen consumos para este dispositivo entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoElectricidadId>> ElectricidadsFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 Electricidad entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoElectricidadId query = new();

            try
            { 
                var Electricidad = await _context.Electricidad.FindAsync(id);
                if (Electricidad == null || currentUser.OrganizacionId != Electricidad.OrganizacionId)
                {
                    return BadRequest("Este dispositivo eléctrico no existe o no pertenece a esta organización");
                }
                query = (from c in _context.ElectricidadConsumo
                                           join v in _context.Electricidad
                                           on c.ElectricidadId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                           group c by c.ElectricidadId into g
                                           select new ConsumoElectricidadId()
                                           {
                                               Total_consumido = g.Sum(r => r.Consumo),
                                               Total_Kilovatios = g.Sum(r => r.Kwh),
                                           }).Single();
            } 
            catch(Exception e)
            {
                return BadRequest($"No existen consumos para el dispositivo con id: {id} entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}/mes")]
        public async Task<ActionResult<List<ConsumoMesElectricidad>>> ElectricidadsFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var Electricidad = await _context.Electricidad.FindAsync(id);
            if (Electricidad == null || currentUser.OrganizacionId != Electricidad.OrganizacionId)
            {
                return BadRequest("Este dispositivo eléctrico no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 Electricidad entre dos fechas agrupado por meses
            List<ConsumoMesElectricidad> query = (from c in _context.ElectricidadConsumo
                                      join v in _context.Electricidad
                                      on c.ElectricidadId equals v.Id
                                      where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                      group c by c.FechaInicio.Month into g
                                      orderby g.Key
                                      select new ConsumoMesElectricidad()
                                      {
                                          Consumo_mes = g.Sum(r => r.Consumo),
                                          Kilovatios_mes = g.Sum(r => r.Kwh),
                                          Mes = g.Key,
                                          Meses = Metodos.ObtenerNombreMes(g.Key)
                                      }).ToList();
            if (query.Count > 0)
            {
                return query;

            }
            else return BadRequest($"No existen consumos para el dispositivo con id: {id} entre estas fechas");
        }
    }
}
