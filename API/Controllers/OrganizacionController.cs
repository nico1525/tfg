using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using AutoMapper;
using API.Models.Context;
using API.Authorization;
using API.Models.Autentificacion;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private IJwtUtils _jwtUtils;


        public OrganizacionController(DatabaseContext context, IMapper mapper, IJwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetOrganizaciones()
        {
            var currentUser = (Organizacion)HttpContext.Items["Organizacion"];

            if (currentUser.Role != Role.Admin)
            {
                var org = await _context.Organizacion.FindAsync(currentUser.Id);
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

        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorization.Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizacion>> GetOrganizacion(int id)
        {
            if (_context.Organizacion == null)
            {
                return NotFound();
            }
            var organizacion = await _context.Organizacion.FindAsync(id);

            if (organizacion == null)
            {
                return NotFound("No existe la organización con id " + id);
            }

            return organizacion;
        }

        [HttpPut]
        public async Task<IActionResult> PutOrganizacion(OrganizacionModifyDTO organizacion)
        {
            var currentOrg = (Organizacion)HttpContext.Items["Organizacion"];

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

        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorization.Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizacionById(int id, OrganizacionModifyDTO organizacion)
        {
            var currentOrg = await _context.Organizacion.FindAsync(id);

            if (_context.Organizacion.Any(e => e.NombreOrg == organizacion.NombreOrg && e.Id != currentOrg.Id))
            {
                return BadRequest("Ya existe una organización con ese nombre");
            }
            else
            {
                if (organizacion.NombreOrg != null) currentOrg.NombreOrg = organizacion.NombreOrg;
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
            //currentOrg.Role = organizacion.Role;

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
                return StatusCode(400, "Este email ya está registrado");

            Organizacion org = _mapper.Map<Organizacion>(organizacion);

            _context.Organizacion.Add(org);
            await _context.SaveChangesAsync();

            return Ok("Organización creada correctamente");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<Organizacion> LoginOrganizacion(LoginRequest loginData)
        {

            var org = _context.Organizacion.SingleOrDefault(x => x.Email == loginData.Email);

            // validate
            if (org == null || loginData.Contraseña != org.Contraseña)
                return BadRequest("Usuario incorrecto");

            // authentication successful
            var response = _mapper.Map<LoginResponse>(org);
            response.Token = _jwtUtils.GenerateToken(org);
            return Ok(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorization.Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizacion(int id)
        {
            if (_context.Organizacion == null)
            {
                return NotFound();
            }
            var organizacion = await _context.Organizacion.FindAsync(id);
            if (organizacion == null)
            {
                return BadRequest("No existe una organización con este id");
            }

            _context.Organizacion.Remove(organizacion);
            await _context.SaveChangesAsync();

            return Ok("Organización eliminada correctamente"); 
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAllOrgs()
        {
            if (_context.Organizacion == null)
            {
                return NotFound();
            }

            List<Organizacion> listorgs =  await _context.Organizacion.ToListAsync();

            foreach (var org in listorgs)
            {
                _context.Organizacion.Remove(org);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas las organizaciones eliminadas correctamente");
        }

        
    }
}
