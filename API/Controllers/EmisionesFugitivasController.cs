using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;
using API.Helpers;
using AutoMapper;
using API.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/Organizacion/[controller]")]
    [ApiController]
    public class EmisionesFugitivasController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public EmisionesFugitivasController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetEmisionesFugitivas()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<EmisionesFugitivas> listatodosemisioness = await _context.EmisionesFugitivas.ToListAsync();
            List<EmisionesFugitivasDTO> listaemisionessorg = new();

            foreach (var emisiones in listatodosemisioness)
            {
                if (emisiones.OrganizacionId == currentUser.OrganizacionId)
                {
                    EmisionesFugitivasDTO emisionesDTO = _mapper.Map<EmisionesFugitivasDTO>(emisiones);
                    listaemisionessorg.Add(emisionesDTO);
                }
            }

            if (currentUser.Role != Role.SuperAdmin) { return listaemisionessorg; }
            else { return listatodosemisioness; }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmisionesFugitivasDTO>> GetEmisionesFugitivasById(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            EmisionesFugitivas EmisionesFugitivas = await _context.EmisionesFugitivas.FindAsync(id);
            EmisionesFugitivasDTO EmisionesFugitivasDTO = new();
            if (EmisionesFugitivas.OrganizacionId == currentUser.OrganizacionId)
            {
                EmisionesFugitivasDTO = _mapper.Map<EmisionesFugitivasDTO>(EmisionesFugitivas);
            }
            else
            {
                return BadRequest("Esta emisión fugitiva no existe o no pertenece a esta organización");
            }
            return EmisionesFugitivasDTO;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmisionesFugitivas(int id, EmisionesFugitivasModifyDTO emisiones)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var emisionesChange = await _context.EmisionesFugitivas.FindAsync(id);
                if (currentUser.OrganizacionId != emisionesChange.OrganizacionId)
                {
                    return BadRequest("Este equipo o fuga no existe o no pertenece a esta organización");
                }
                if (emisiones.Edificio != null) emisionesChange.Edificio = emisiones.Edificio;
                if (emisiones.NombreEquipo != null) emisionesChange.NombreEquipo = emisiones.NombreEquipo;

                await _context.SaveChangesAsync();

                return Ok("Equipo o fuga modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún equipo o fuga");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmisionesFugitivas>> PostEmisionesFugitivas(EmisionesFugitivasCreateDTO emisionesDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            EmisionesFugitivas emisiones = _mapper.Map<EmisionesFugitivas>(emisionesDTO);

            emisiones.OrganizacionId = currentUser.OrganizacionId;
            emisiones.OrganizacionRef = currentUser.OrganizacionRef;
            _context.EmisionesFugitivas.Add(emisiones);
            await _context.SaveChangesAsync();
            return Ok("Equipo de refrigeración/climatización o fuga de gas añadido creado correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmisionesFugitivas(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var emisionesDelete = await _context.EmisionesFugitivas.FindAsync(id);
                if (currentUser.OrganizacionId != emisionesDelete.OrganizacionId)
                {
                    return BadRequest("Este equipo o fuga no existe o no pertenece a esta organización");
                }
                _context.EmisionesFugitivas.Remove(emisionesDelete);
                await _context.SaveChangesAsync();

                return Ok("Equipo o fuga eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún equipo o fuga");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllEmisionesFugitivass()
        {
            List<EmisionesFugitivas> emisioneslist = await _context.EmisionesFugitivas.ToListAsync();

            foreach (var emisiones in emisioneslist)
            {
                _context.EmisionesFugitivas.Remove(emisiones);
            }
            await _context.SaveChangesAsync();

            return Ok("Todos los equipos o fugas eliminados correctamente");
        }
    }
}
