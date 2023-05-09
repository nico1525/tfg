using API.Authorization;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionController : ControllerBase
    {
        [HttpGet("combustiblevehiculo")]
        public List<TipoTransporte> GetTipoCombustible() {
            TipoTransporte tr = new() { TipoCombustible = new List<string?>() { "E5", "E10", "E85", "E100", "B7", "B10", "B20", "B30", "B100", "LPG", "CNG"}, Transporte = "M1" };
            TipoTransporte tr1 = new() { TipoCombustible = new List<string?>() { "E5", "E10", "E85", "E100", "B7", "B10", "B20", "B30", "B100"}, Transporte = "N1" };
            TipoTransporte tr2 = new() { TipoCombustible = new List<string?>() { "E5", "E10", "E85", "E100", "B7", "B10", "B20", "B30", "B100", "CNG"}, Transporte = "N2, N3, M2, M3" };
            TipoTransporte tr3 = new() { TipoCombustible = new List<string?>() { "E5", "E10", "E85", "E100"}, Transporte = "L" };
            List<TipoTransporte> transporte = new() {
                tr,
                tr1,
                tr2,
                tr3
            };
            return transporte;
        }

        [HttpGet("combustibletransporte")]
        public List<TipoTransporte> GetCombustibleTransporte()
        {
            TipoTransporte tr = new() { TipoCombustible = new List<string?>() { "gasoleo"}, Transporte = "ferrocarril" };
            TipoTransporte tr1 = new() { TipoCombustible = new List<string?>() { "gasoleo", "fueloleo" }, Transporte = "maritimo" };
            TipoTransporte tr2 = new() { TipoCombustible = new List<string?>() { "queroseno", "gasolinaaviacion" }, Transporte = "aereo" };

            List<TipoTransporte> transporte = new() {
                tr,
                tr1,
                tr2
            };
            return transporte;
        }
    }

    public class TipoTransporte {
        public string? Transporte { get; set; }
        public List<string?>? TipoCombustible { get; set; }
       
    }
}
