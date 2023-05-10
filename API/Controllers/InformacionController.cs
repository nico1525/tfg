using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("combustiblemaquinaria")]
        public List<TipoMaquinaria> GetTipoCombustibleMaquinaria()
        {
            TipoMaquinaria tr = new() { TipoCombustible = new List<string?>() { "gasoleoB", "B7", "B10", "B20", "B30", "B100" }, Maquinaria = "agricola" };
            TipoMaquinaria tr1 = new() { TipoCombustible = new List<string?>() { "gasoleoB", "E5", "E10", "E85", "E100", "B7", "B10", "B20", "B30", "B100" }, Maquinaria = "forestal" };
            TipoMaquinaria tr2 = new() { TipoCombustible = new List<string?>() { "gasoleoB", "E5", "E10", "E85", "E100", "B7", "B10", "B20", "B30", "B100" }, Maquinaria = "industrial" };
            List<TipoMaquinaria> transporte = new() {
                tr,
                tr1,
                tr2
            };
            return transporte;
        }

        [HttpGet("combustibleinstalacionesfijas")]
        public CombustibleFijas GetTipoCombustibleInstalacionesFijas()
        {
            CombustibleFijas tr = new()
            {
                tipo = "Instalaciones Fijas",
                TipoCombustible = new List<string?>() { "gasoleoc",
                                                        "gasoleob",
                                                        "gasnatural",
                                                        "fueloleo",
                                                        "LPG",
                                                        "queroseno",
                                                        "gaspropano",
                                                        "gasbutano",
                                                        "gasmanufacturado",
                                                        "biogas",
                                                       "biomasamadera",
                                                        "biomasapellets",
                                                        "biomasaastillas",
                                                        "biomasaserrinesvirutas",
                                                        "biomasacascaraf.secos",
                                                        "biomasahuesoaceituna",
                                                        "carbonvegetal",
                                                        "coquedepetroleo",
                                                        "coquedecarbon",
                                                        "hullayantracita",
                                                        "hullassubituminosas"}
            };
            return tr;

        }
    }

    public class CombustibleFijas
    {
        public string? tipo { get; set; }
        public List<string?>? TipoCombustible { get; set; }

    }

    public class TipoTransporte {
        public string? Transporte { get; set; }
        public List<string?>? TipoCombustible { get; set; }
       
    }

    public class TipoMaquinaria
    {
        public string? Maquinaria { get; set; }
        public List<string?>? TipoCombustible { get; set; }

    }
}
