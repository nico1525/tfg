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
        [HttpGet("tipocombustible")]
        public List<string> GetTipoCombustible() {
            List<string> tipocombustiblelist = new() {
                "E5", "E10", "E85", "E100", "B7", "B10", "B20", "B30", "B100", "LPG", "CNG"
            };
            return tipocombustiblelist;
        }

        [HttpGet("categoriavehiculo")]
        public List<string> GetCategoriaVehiculo()
        {
            List<string>categoriavehiculolist = new() {
                "M1", "N1", "N2", "N3", "M2", "M3", "L"
            };
            return categoriavehiculolist;
        }
    }
}
