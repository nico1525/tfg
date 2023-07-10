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
using static API.Controllers.InformesControllers.InformesVehiculoController;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/Vehiculo")]
    [ApiController]
    public class InformesVehiculoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesVehiculoController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoCombustibleId>> AllVehiculoFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los vehiculos entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoCombustibleId query = new();
            try
            {
                query = (from c in _context.VehiculoConsumo
                                           join v in _context.Vehiculo
                                           on c.VehiculoId equals v.Id
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
        public async Task<ActionResult<ConsumoCombustibleId>> VehiculosFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 vehiculo entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoCombustibleId query = new();

            try
            { 
                var vehiculo = await _context.Vehiculo.FindAsync(id);
                if (vehiculo == null || currentUser.OrganizacionId != vehiculo.OrganizacionId)
                {
                    return BadRequest("Este vehículo no existe o no pertenece a esta organización");
                }
                query = (from c in _context.VehiculoConsumo
                                           join v in _context.Vehiculo
                                           on c.VehiculoId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                           group c by c.VehiculoId into g
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
        public async Task<ActionResult<List<ConsumoMes>>> VehiculosFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (vehiculo == null || currentUser.OrganizacionId != vehiculo.OrganizacionId)
            {
                return BadRequest("Este vehiculo no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 vehiculo entre dos fechas agrupado por meses
            List<ConsumoMes> query = (from c in _context.VehiculoConsumo
                                      join v in _context.Vehiculo
                                      on c.VehiculoId equals v.Id
                                      where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                      group c by c.FechaInicio.Month into g
                                      orderby g.Key
                                      select new ConsumoMes()
                                      {
                                          Consumo_mes = g.Sum(r => r.Consumo),
                                          Combustible_mes = g.Sum(r => r.CantidadCombustible),
                                          Mes = g.Key,
                                          Meses = ObtenerNombreMes(g.Key)
                                      }).ToList();
            return query;
        }

        public static string ObtenerNombreMes(int numeroMes)
        {
            string[] nombresMeses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            if (numeroMes >= 1 && numeroMes <= 12)
            {
                return nombresMeses[numeroMes - 1];
            }
            else { return "mes inválido"; }
        }
    }
}
