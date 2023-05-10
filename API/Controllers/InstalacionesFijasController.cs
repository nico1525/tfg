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
    [Route("api/Organizacion/InstalacionesFijas")]
    [ApiController]
    public class InstalacionesFijasController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public InstalacionesFijasController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetInstalacionesFijas()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<InstalacionesFijas> listatodosinstalacionesFijas = await _context.InstalacionesFijas.ToListAsync();
            List<InstalacionesFijasDTO> listainstalacionesFijasorg = new();

            foreach (var instalacionesFijas in listatodosinstalacionesFijas)
            {
                if (instalacionesFijas.OrganizacionId == currentUser.OrganizacionId)
                {
                    InstalacionesFijasDTO instalacionesFijasDTO = _mapper.Map<InstalacionesFijasDTO>(instalacionesFijas);
                    listainstalacionesFijasorg.Add(instalacionesFijasDTO);
                }
            }

            if (currentUser.Role != Role.SuperAdmin) { return listainstalacionesFijasorg; }
            else { return listatodosinstalacionesFijas; }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstalacionesFijas(int id, InstalacionesFijasModifyDTO instalacionesFijas)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var instalacionesFijasChange = await _context.InstalacionesFijas.FindAsync(id);
                if (currentUser.OrganizacionId != instalacionesFijasChange.OrganizacionId)
                {
                    return BadRequest("Esta instalación fija no existe o no pertenece a esta organización");
                }

                if (instalacionesFijas.Edificio != null) instalacionesFijasChange.Edificio = instalacionesFijas.Edificio;
                if (instalacionesFijas.Nombre != null) instalacionesFijasChange.Nombre = instalacionesFijas.Nombre;

                await _context.SaveChangesAsync();

                return Ok("Instalación fija modificada correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna instalación fija");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InstalacionesFijas>> PostInstalacionesFijas(InstalacionesFijasCreateDTO instalacionesFijasDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            InstalacionesFijas instalacionesFijas = _mapper.Map<InstalacionesFijas>(instalacionesFijasDTO);

            instalacionesFijas.OrganizacionId = currentUser.OrganizacionId;
            instalacionesFijas.OrganizacionRef = currentUser.OrganizacionRef;
            _context.InstalacionesFijas.Add(instalacionesFijas);
            await _context.SaveChangesAsync();
            return Ok("Instalación fija creada correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalacionesFijas(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var instalacionesFijasDelete = await _context.InstalacionesFijas.FindAsync(id);
                if (currentUser.OrganizacionId != instalacionesFijasDelete.OrganizacionId)
                {
                    return BadRequest("Esta instalación fija no existe o no pertenece a esta organización");
                }
                _context.InstalacionesFijas.Remove(instalacionesFijasDelete);
                await _context.SaveChangesAsync();

                return Ok("Instalación fija eliminada correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna instalación fija");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllInstalacionesFijass()
        {
            List<InstalacionesFijas> instalacionesFijaslist = await _context.InstalacionesFijas.ToListAsync();

            foreach (var instalacionesFijas in instalacionesFijaslist)
            {
                _context.InstalacionesFijas.Remove(instalacionesFijas);
            }
            await _context.SaveChangesAsync();

            return Ok("Todas las instalaciones fijas eliminadas correctamente");
        }
    }
}

