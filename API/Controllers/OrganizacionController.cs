using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using AutoMapper;
using API.Models.Context;
using API.Authorization;
using API.Models.Autentificacion;
using API.Helpers;
using System.ComponentModel.DataAnnotations;
using API.Models.Consumos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrganizacionController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetOrganizaciones()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (currentUser.Role == Role.OrgAdmin || currentUser.Role == Role.User)
            {
                var org = await _context.Organizacion.FindAsync(currentUser.OrganizacionId);
                OrganizacionDTO orgDTO = _mapper.Map<OrganizacionDTO>(org);
                List<OrganizacionDTO> listorg = new()
            {
                    orgDTO
            };
                return listorg;
            }
            else
            {
                return await _context.Organizacion.ToListAsync();
            }

        }

       
        [Authorize(Role.OrgAdmin)]
        [HttpPut]
        public async Task<IActionResult> PutOrganizacion(OrganizacionModifyDTO organizacion)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var currentOrg = currentUser.OrganizacionRef;

            var antiguaorganizacion = await _context.Organizacion.FindAsync(currentOrg.Id);

            if (_context.Organizacion.Any(e => e.NombreOrg == organizacion.NombreOrg && e.Id != currentOrg.Id))
            {
                return BadRequest("Ya existe una organización con ese nombre");
            }
            else
            {
                if(organizacion.NombreOrg != null) currentOrg.NombreOrg = organizacion.NombreOrg;
            }
            if (_context.Organizacion.Any(e => e.Email == organizacion.Email && e.Id != currentOrg.Id))
            {
                return BadRequest("Ya existe una organización con ese email");
            }
            else
            {
                if (organizacion.Email != null) currentOrg.Email = organizacion.Email;
            }
            if (organizacion.Direccion != null) currentOrg.Direccion = organizacion.Direccion;
            if (organizacion.Contraseña != null) currentOrg.Contraseña = organizacion.Contraseña;

            await _context.SaveChangesAsync();

            return Ok("Organización modificada correctamente");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Organizacion>> RegistrarOrganizacion(OrganizacionCreateDTO organizacion)
        {

            if (_context.Organizacion.Any(o => o.NombreOrg == organizacion.NombreOrg))
                return StatusCode(400, "Este nombre de esta Organización ya está registrado");
            if (_context.Organizacion.Any(o => o.Email == organizacion.Email))
                return StatusCode(400, "Este email de Organización ya está registrado");

            Organizacion org = _mapper.Map<Organizacion>(organizacion);


            _context.Organizacion.Add(org);
            await _context.SaveChangesAsync();

            Usuario usuario = new()
            {
                NombreApellidos = "admin",
                Email = org.Email,
                Contraseña = org.Contraseña,
                OrganizacionId = org.Id,
                OrganizacionRef = org,
                Role = Role.OrgAdmin
            };
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Organización creada correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrganizacion()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            var currentOrg = currentUser.OrganizacionRef;

            _context.Organizacion.Remove(currentOrg);
            await _context.SaveChangesAsync();

            return Ok("Organización eliminada correctamente"); 
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllOrgs()
        {
            List<Organizacion> listorgs =  await _context.Organizacion.ToListAsync();

            foreach (var org in listorgs)
            {
                _context.Organizacion.Remove(org);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas las organizaciones eliminadas correctamente");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizacion>> DeleteOrganizacionById(int id)
        {
            try { 
            var organizacion = await _context.Organizacion.FindAsync(id);

            if (organizacion == null)
            {
                return NotFound("No existe la organización con id " + id);
            }

            return organizacion;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna organización");
            }
        }

        [Authorize]
        [HttpGet("consumos")]
        public async Task<IEnumerable<float>> GetConsumosbyDate(DateTime ini, DateTime fin)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            int orgid = currentUser.OrganizacionId;
            List<float> consumolist = new List<float>();

            List<VehiculoConsumo> listatodosvehiculos = await _context.VehiculoConsumo.ToListAsync();
            foreach (var vehiculo in listatodosvehiculos)
            {
                var vehiculoref = await _context.Vehiculo.FindAsync(vehiculo.VehiculoId);
                if (orgid == vehiculoref.OrganizacionId && vehiculo.FechaInicio >= ini && vehiculo.FechaFin <= fin)
                {
                    consumolist.Add(vehiculo.Consumo);
                }
            }

            List<TransporteConsumo> listatodosTransportes = await _context.TransporteConsumo.ToListAsync();
            foreach (var Transporte in listatodosTransportes)
            {
                var Transporteref = await _context.Transporte.FindAsync(Transporte.TransporteId);
                if (orgid == Transporteref.OrganizacionId && Transporte.FechaInicio >= ini && Transporte.FechaFin <= fin)
                {
                    consumolist.Add(Transporte.Consumo);
                }
            }

            List<MaquinariaConsumo> listatodosMaquinarias = await _context.MaquinariaConsumo.ToListAsync();
            foreach (var Maquinaria in listatodosMaquinarias)
            {
                var Maquinariaref = await _context.Maquinaria.FindAsync(Maquinaria.MaquinariaId);
                if (orgid == Maquinariaref.OrganizacionId && Maquinaria.FechaInicio >= ini && Maquinaria.FechaFin <= fin)
                {
                    consumolist.Add(Maquinaria.Consumo);
                }
            }

            List<EmisionesFugitivasConsumo> listatodosEmisionesFugitivass = await _context.EmisionesFugitivasConsumo.ToListAsync();
            foreach (var EmisionesFugitivas in listatodosEmisionesFugitivass)
            {
                var EmisionesFugitivasref = await _context.EmisionesFugitivas.FindAsync(EmisionesFugitivas.EmisionesFugitivasId);
                if (orgid == EmisionesFugitivasref.OrganizacionId && EmisionesFugitivas.FechaInicio >= ini && EmisionesFugitivas.FechaFin <= fin)
                {
                    consumolist.Add(EmisionesFugitivas.Consumo);
                }
            }

            List<InstalacionesFijasConsumo> listatodosInstalacionesFijass = await _context.InstalacionesFijasConsumo.ToListAsync();
            foreach (var InstalacionesFijas in listatodosInstalacionesFijass)
            {
                var InstalacionesFijasref = await _context.InstalacionesFijas.FindAsync(InstalacionesFijas.InstalacionesFijasId);
                if (orgid == InstalacionesFijasref.OrganizacionId && InstalacionesFijas.FechaInicio >= ini && InstalacionesFijas.FechaFin <= fin)
                {
                    consumolist.Add(InstalacionesFijas.Consumo);
                }
            }
            List<OtrosConsumos> listatodosOtrosConsumoss = await _context.OtrosConsumos.ToListAsync();
            foreach (var OtrosConsumos in listatodosOtrosConsumoss)
            {
                if (orgid == OtrosConsumos.OrganizacionId && OtrosConsumos.FechaInicio >= ini && OtrosConsumos.FechaFin <= fin)
                {
                    consumolist.Add(OtrosConsumos.Consumo);
                }
            }

            return consumolist;
        }


    }
}
