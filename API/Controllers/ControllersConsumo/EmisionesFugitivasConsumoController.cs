using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Consumos;
using API.Models.Context;
using API.Authorization;
using API.Calculos;
using API.Models;
using AutoMapper;

namespace API.Controllers.ControllersConsumo
{
    [Authorize]
    [Route("api/Organizacion/EmisionesFugitivas/Consumo")]
    [ApiController]
    public class EmisionesFugitivasConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public EmisionesFugitivasConsumoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmisionesFugitivasConsumoDTO>>> GetEmisionesFugitivasConsumo()
        {
            //Devuelve todos los consumos de emisiones de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.EmisionesFugitivasConsumo.ToListAsync();
            List<EmisionesFugitivasConsumoDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                var emisiones = await _context.EmisionesFugitivas.FindAsync(item.EmisionesFugitivasId);

                if (emisiones.OrganizacionId == currentUser.OrganizacionId)
                {
                    EmisionesFugitivasConsumoDTO emisionesConsumo = _mapper.Map<EmisionesFugitivasConsumoDTO>(item);
                    orgConsumoList.Add(emisionesConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<EmisionesFugitivasConsumoDTO>> GetEmisionesFugitivasConsumoId(int id)
        {
            //Devuelve el consumos por su id
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var consumo = await _context.EmisionesFugitivasConsumo.FindAsync(id);
            EmisionesFugitivasConsumoDTO consumodto = _mapper.Map<EmisionesFugitivasConsumoDTO>(consumo);

            return consumodto;
        }

        [HttpGet("{emisionesid}")]
        public async Task<ActionResult<IEnumerable<EmisionesFugitivasConsumoDTO>>> GetEmisionesFugitivasConsumo(int id)
        {
            //Devuelve todos los consumos de un emisiones
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var emisiones = await _context.EmisionesFugitivas.FindAsync(id);
                var listconsumos = await _context.EmisionesFugitivasConsumo.ToListAsync();

                if (currentUser.OrganizacionId != emisiones.OrganizacionId)
                {
                    return BadRequest("Este equipo o fuga no pertenece a esta organización");
                }
                List<EmisionesFugitivasConsumoDTO> orgConsumoList = new();

                foreach (var item in listconsumos)
                {
                    var emisionesref = await _context.EmisionesFugitivas.FindAsync(item.EmisionesFugitivasId);
                    if (emisionesref.Id == id)
                    {
                        EmisionesFugitivasConsumoDTO emisionesConsumo = _mapper.Map<EmisionesFugitivasConsumoDTO>(item);
                        orgConsumoList.Add(emisionesConsumo);
                    }
                }
                return orgConsumoList;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún equipo o fuga");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmisionesFugitivasConsumo(int id, EmisionesFugitivasConsumoModifyDTO emisionesConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var emisionesChange = await _context.EmisionesFugitivasConsumo.FindAsync(id);

                var emisiones = await _context.EmisionesFugitivas.FindAsync(emisionesChange.EmisionesFugitivasId);
                DateTime test = new DateTime(1, 1, 1, 0, 0, 0, 0);
                if (currentUser.OrganizacionId != emisiones.OrganizacionId)
                {
                    return BadRequest("Este consumo de equipo o fuga no pertenece a esta organización");
                }
                if (emisionesConsumo.Edificio != null) emisionesChange.Edificio = emisionesConsumo.Edificio;
                if (emisionesConsumo.Gas != null) emisionesChange.Gas = emisionesConsumo.Gas;
                if (emisionesConsumo.Recarga > 0) emisionesChange.Recarga = emisionesConsumo.Recarga;
                if (emisionesConsumo.FechaInicio != test) emisionesChange.FechaInicio = emisionesConsumo.FechaInicio;
                if (emisionesConsumo.FechaFin != test) emisionesChange.FechaFin = emisionesConsumo.FechaFin;

                if (DateTime.Compare(emisionesChange.FechaInicio, emisionesChange.FechaFin) > 0)
                {
                    return BadRequest("La fecha final debe ser superior a la fecha inicial");
                }

                try
                {
                    emisionesChange.Consumo = CalculoEmisionesFugitivas.CalculoConsumoEmisionesFugitivas(emisionesChange, _context);
                }
                catch (NullReferenceException ex)
                {
                    return BadRequest("Este Tipo de Gas no es válido");
                }
                await _context.SaveChangesAsync();

                return Ok("Consumo del equipo o fuga con Id: " + emisiones.Id + " modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de equipo o fuga");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmisionesFugitivasConsumo>> PostEmisionesFugitivasConsumo(EmisionesFugitivasConsumoCreateDTO emisionesConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(emisionesConsumoDTO.FechaInicio, emisionesConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            List<EmisionesFugitivas> listemisioness = await _context.EmisionesFugitivas.ToListAsync();
            foreach (var emisiones in listemisioness)
            {
                if (emisionesConsumoDTO.EmisionesFugitivasId == emisiones.Id && emisiones.OrganizacionId == currentUser.OrganizacionId)
                {
                    EmisionesFugitivasConsumo emisionesConsumo = _mapper.Map<EmisionesFugitivasConsumo>(emisionesConsumoDTO);
                    try
                    {
                        emisionesConsumo.Consumo = CalculoEmisionesFugitivas.CalculoConsumoEmisionesFugitivas(emisionesConsumo, _context);
                    }
                    catch (NullReferenceException ex)
                    {
                        return BadRequest("Este Tipo de Gas no es válido");
                    }
                    emisionesConsumo.EmisionesFugitivasId = emisiones.Id;

                    _context.EmisionesFugitivasConsumo.Add(emisionesConsumo);
                    await _context.SaveChangesAsync();
                    return Ok("Consumo de equipo o fuga creado correctamente");
                }

            }
            return BadRequest("No existe un equipo o fuga con este id registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmisionesFugitivasConsumo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var emisionesDelete = await _context.EmisionesFugitivasConsumo.FindAsync(id);
                var EmisionesFugitivas = await _context.EmisionesFugitivas.FindAsync(emisionesDelete.EmisionesFugitivasId);

                if (currentUser.OrganizacionId != EmisionesFugitivas.OrganizacionId)
                {
                    return BadRequest("Este consumo de equipo o fuga no existe o no pertenece a esta organización");
                }
                _context.EmisionesFugitivasConsumo.Remove(emisionesDelete);
                await _context.SaveChangesAsync();

                return Ok("Consumo de equipo o fuga eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de equipo o fuga");
            }
        }
    }
}
