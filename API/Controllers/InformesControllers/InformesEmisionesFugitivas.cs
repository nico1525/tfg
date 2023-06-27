using API.Models.Query_Models;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Authorization;
using API.Models.Context;
using System.Linq.Expressions;
using API.Helpers;

namespace API.Controllers.InformesControllers
{
    [Route("api/Informes/EmisionesFijas")]
    [ApiController]
    public class InformesEmisionesFijasController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public InformesEmisionesFijasController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConsumoEmisionesFugitivasId>> AllEmisionesFugitivasFechas(DateTime fechaini, DateTime fechafin)
        {
            //El consumo total de todos los EmisionesFugitivass entre dos fechas
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoEmisionesFugitivasId query = new();
            try
            {
                query = (from c in _context.EmisionesFugitivasConsumo
                                           join v in _context.EmisionesFugitivas
                                           on c.EmisionesFugitivasId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.OrganizacionId == currentUser.OrganizacionId
                                           group c by v.OrganizacionId into g
                                           select new ConsumoEmisionesFugitivasId()
                                           {
                                               Total_consumido = g.Sum(r => r.Consumo),
                                           }).Single();
            } catch(Exception e)
            {
                return BadRequest("No existen consumos para este dispositivo entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoEmisionesFugitivasId>> EmisionesFugitivassFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            //El consumo total de 1 EmisionesFugitivas entre dos fechas

            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            ConsumoEmisionesFugitivasId query = new();

            try
            { 
                var EmisionesFugitivas = await _context.EmisionesFugitivas.FindAsync(id);
                if (currentUser.OrganizacionId != EmisionesFugitivas.OrganizacionId)
                {
                    return BadRequest("Este EmisionesFugitivas no existe o no pertenece a esta organización");
                }
                query = (from c in _context.EmisionesFugitivasConsumo
                                           join v in _context.EmisionesFugitivas
                                           on c.EmisionesFugitivasId equals v.Id
                                           where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                           group c by c.EmisionesFugitivasId into g
                                           select new ConsumoEmisionesFugitivasId()
                                           {
                                               Total_consumido = g.Sum(r => r.Consumo),
                                           }).Single();
            } 
            catch(Exception e)
            {
                return BadRequest($"No existen consumos para el dispositivo con id: {id} entre estas fechas");
            }
            return query;
        }

        [HttpGet("{id}/mes")]
        public async Task<ActionResult<List<ConsumoMes>>> EmisionesFugitivassFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var EmisionesFugitivas = await _context.EmisionesFugitivas.FindAsync(id);
            if (currentUser.OrganizacionId != EmisionesFugitivas.OrganizacionId)
            {
                return BadRequest("Este EmisionesFugitivas no existe o no pertenece a esta organización");
            }
            //El consumo total de 1 EmisionesFugitivas entre dos fechas agrupado por meses
            List<ConsumoMes> query = (from c in _context.EmisionesFugitivasConsumo
                                      join v in _context.EmisionesFugitivas
                                      on c.EmisionesFugitivasId equals v.Id
                                      where c.FechaInicio >= fechaini && c.FechaInicio <= fechafin && v.Id == id
                                      group c by c.FechaInicio.Month into g
                                      orderby g.Key
                                      select new ConsumoMes()
                                      {
                                          Consumo_mes = g.Sum(r => r.Consumo),
                                          Mes = g.Key
                                      }).ToList();
            return query;
        }
    }
}
