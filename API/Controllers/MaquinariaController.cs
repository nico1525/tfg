using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;
using API.Authorization;
using API.Helpers;
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    [Route("api/Organizacion/Maquinaria")]
    [ApiController]
    public class MaquinariaController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public MaquinariaController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetMaquinaria()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<Maquinaria> listatodosmaquinarias = await _context.Maquinaria.ToListAsync();
            List<MaquinariaDTO> listamaquinariasorg = new();

            foreach (var maquinaria in listatodosmaquinarias)
            {
                if (maquinaria.OrganizacionId == currentUser.OrganizacionId)
                {
                    MaquinariaDTO maquinariaDTO = _mapper.Map<MaquinariaDTO>(maquinaria);
                    listamaquinariasorg.Add(maquinariaDTO);
                }
            }

            if (currentUser.Role != Role.SuperAdmin) { return listamaquinariasorg; }
            else { return listatodosmaquinarias; }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MaquinariaDTO>> GetMaquinariaById(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Maquinaria Maquinaria = await _context.Maquinaria.FindAsync(id);
            MaquinariaDTO MaquinariaDTO = new();
            if (Maquinaria.OrganizacionId == currentUser.OrganizacionId)
            {
                MaquinariaDTO = _mapper.Map<MaquinariaDTO>(Maquinaria);
            }
            else
            {
                return BadRequest("Esta Maquinaria no existe o no pertenece a esta organización");
            }
            return MaquinariaDTO;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquinaria(int id, MaquinariaModifyDTO maquinaria)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var maquinariaChange = await _context.Maquinaria.FindAsync(id);
                if (currentUser.OrganizacionId != maquinariaChange.OrganizacionId)
                {
                    return BadRequest("Esta maquinaria no existe o no pertenece a esta organización");
                }

                if (maquinaria.Edificio != null) maquinariaChange.Edificio = maquinaria.Edificio;
                if (maquinaria.Nombre != null) maquinariaChange.Nombre = maquinaria.Nombre;

                await _context.SaveChangesAsync();

                return Ok("Maquinaria modificada correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna maquinaria");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Maquinaria>> PostMaquinaria(MaquinariaCreateDTO maquinariaDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Maquinaria maquinaria = _mapper.Map<Maquinaria>(maquinariaDTO);

            maquinaria.OrganizacionId = currentUser.OrganizacionId;
            maquinaria.OrganizacionRef = currentUser.OrganizacionRef;
            _context.Maquinaria.Add(maquinaria);
            await _context.SaveChangesAsync();
            return Ok("Maquinaria creada correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquinaria(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var maquinariaDelete = await _context.Maquinaria.FindAsync(id);
                if (currentUser.OrganizacionId != maquinariaDelete.OrganizacionId)
                {
                    return BadRequest("Esta maquinaria no existe o no pertenece a esta organización");
                }
                _context.Maquinaria.Remove(maquinariaDelete);
                await _context.SaveChangesAsync();

                return Ok("Maquinaria eliminada correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna maquinaria");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllMaquinarias()
        {
            List<Maquinaria> maquinarialist = await _context.Maquinaria.ToListAsync();

            foreach (var maquinaria in maquinarialist)
            {
                _context.Maquinaria.Remove(maquinaria);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas las maquinarias eliminadas correctamente");
        }
    }
}
