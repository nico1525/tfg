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
            return null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoOtrosConsumosId>> OtrosConsumossFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            return null;
        }

        [HttpGet("{id}/mes")]
        public async Task<ActionResult<List<ConsumoMes>>> OtrosConsumossFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            return null;
        }
    }
}
