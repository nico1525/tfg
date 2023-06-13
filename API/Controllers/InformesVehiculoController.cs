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
using static API.Controllers.InformesVehiculoController;

namespace API.Controllers
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
        public ConsumoVehiculoId AllVehiculoFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los vehiculos entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            ConsumoVehiculoId query = (from c in _context.VehiculoConsumo
                        join v in _context.Vehiculo
                        on c.VehiculoId equals v.Id
                        where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.OrganizacionId == currentUser.OrganizacionId
                        group c by v.OrganizacionId into g
                        select new ConsumoVehiculoId()
                        {
                            Total_consumido = g.Sum(r => r.Consumo),
                            Total_combustible = g.Sum(r => r.CantidadCombustible)
                        }).Single();
            return query;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoVehiculoId>> VehiculosFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 vehiculo entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (currentUser.OrganizacionId != vehiculo.OrganizacionId)
            {
                return BadRequest("Este vehiculo no existe o no pertenece a esta organización");
            }
            ConsumoVehiculoId query = (from c in _context.VehiculoConsumo
                        join v in _context.Vehiculo
                        on c.VehiculoId equals v.Id
                        where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                        group c by c.VehiculoId into g
                        select new ConsumoVehiculoId()
                        {
                            Total_consumido = g.Sum(r => r.Consumo),
                            Total_combustible = g.Sum(r => r.CantidadCombustible),
                        }).Single();
            return query;
        }

        [HttpGet("{id}/mes")]
        public async Task<ActionResult<List<ConsumoMes>>> VehiculosFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (currentUser.OrganizacionId != vehiculo.OrganizacionId)
            {
                return BadRequest("Este vehiculo no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 vehiculo entre dos fechas agrupado por meses
            List<ConsumoMes> query = (from c in _context.VehiculoConsumo
                         join v in _context.Vehiculo 
                         on c.VehiculoId equals v.Id
                         where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                         group c by c.FechaInicio.Month into g
                        select new ConsumoMes()
                        {
                            Consumo_mes = g.Sum(r => r.Consumo),
                            Combustible_mes = g.Sum(r => r.CantidadCombustible),
                            Mes = g.Key
                        }).ToList();
            return query;
        }

        public class ConsumoMes
        {
            public int Mes { get; set; }
            public float Consumo_mes { get; set; }
            public float Combustible_mes { get; set; }
        }
        public class ConsumoVehiculoId  
        {
            public float Total_consumido { get; set; }
            public float Total_combustible { get; set; }
        }
    }
}
