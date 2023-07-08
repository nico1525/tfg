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
        [HttpGet("gasesemisionesfugitivas")]
        public GasesEmisionesFugitivas GetTipoGasesEmisionesFugitivas()
        {
            GasesEmisionesFugitivas tr = new()
            {
                tipo = "Emisiones Fugitivas",
                NombreGas = new List<string?>() { 
                                            "HFC-23",
	                                        "HFC-32",
	                                        "HFC-41",
	                                        "HFC-125",
	                                        "HFC-134",
	                                        "HFC-134a",
	                                        "HFC-143",
	                                        "HFC-143a",
	                                        "HFC-152",
	                                        "HFC-152a",
	                                        "HFC-161",
	                                        "HFC-227ea",
	                                        "HFC-236cb",
	                                        "HFC-236ea",
	                                        "HFC-236fa",
	                                        "HFC-245ca",
	                                        "HFC-245fa",
	                                        "HFC-365mfc",
	                                        "HFC-43-10mee",
	                                        "R-404A",
	                                        "R-407A",
	                                        "R-407B",
	                                        "R-407C",
	                                        "R-407F",
	                                        "R-410A",
	                                        "R-410B",
	                                        "R-413A",
	                                        "R-417A",
	                                        "R-417B",
	                                        "R-422A",
	                                        "R-422D",
	                                        "R-424A",
	                                        "R-426A",
	                                        "R-427A",
	                                        "R-428A",
	                                        "R-434A",
	                                        "R-437A",
	                                        "R-438A",
	                                        "R-442A",
	                                        "R-449A",
	                                        "R-452A",
	                                        "R-453A",
	                                        "R-507A",
	                                        "dioxidodecarbono",
	                                        "metano",
	                                        "oxidonitroso",
	                                        "hexafluorurodeazufre",
	                                        "trifluorurodenitrogeno",
	                                        "isoflurano",
	                                        "desflurano",
	                                        "sevoflurano",
	                                        "hexafluoroetano"
                }
            };
            return tr;

        }
    }

    public class CombustibleFijas
    {
        public string? tipo { get; set; }
        public List<string?>? TipoCombustible { get; set; }

    }
    public class GasesEmisionesFugitivas
    {
        public string? tipo { get; set; }
        public List<string?>? NombreGas { get; set; }

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
