using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class TransporteController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TransporteController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTransporte()
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            List<Transporte> listatodosransportes = await _context.Transporte.ToListAsync();
            List<TransporteDTO> listatransportesorg = new();

            foreach (var transporte in listatodosransportes)
            {
                if (transporte.OrganizacionId == currentUser.OrganizacionId)
                {
                    TransporteDTO transporteDTO = _mapper.Map<TransporteDTO>(transporte);
                    listatransportesorg.Add(transporteDTO);
                }
            }

            if (currentUser.Role != Role.SuperAdmin) { return listatransportesorg; }
            else { return listatodosransportes; }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TransporteDTO>> GetTransporteById(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            Transporte Transporte = await _context.Transporte.FindAsync(id);
            TransporteDTO TransporteDTO = new();
            if (Transporte.OrganizacionId == currentUser.OrganizacionId)
            {
                TransporteDTO = _mapper.Map<TransporteDTO>(Transporte);
            }
            else
            {
                return BadRequest("Este transporte no existe o no pertenece a esta organización");
            }
            return TransporteDTO;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporte(int id, TransporteModifyDTO transporte)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var transporteChange = await _context.Transporte.FindAsync(id);
                if (currentUser.OrganizacionId != transporteChange.OrganizacionId)
                {
                    return BadRequest("Este transporte no existe o no pertenece a esta organización");
                }
                if (transporte.Edificio != null && transporte.Edificio != "") transporteChange.Edificio = transporte.Edificio;

                await _context.SaveChangesAsync();

                return Ok("Transporte modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún transporte");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Transporte>> PostTransporte(TransporteCreateDTO transporteDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if(transporteDTO.TipoTransporte.Equals("ferrocarril") && !transporteDTO.CombustibleTransporte.Equals("gasoleo"))
            {
                return BadRequest("El transporte ferrocarril solo puede usar el combustible gasoleo");
            }
            if (transporteDTO.TipoTransporte.Equals("maritimo") && (transporteDTO.CombustibleTransporte.Equals("queroseno") || transporteDTO.CombustibleTransporte.Equals("gasolinaaviacion")))
            {
                return BadRequest("El transporte maritimo solo puede usar el combustible gasoleo o fueloleo");
            }
            if (transporteDTO.TipoTransporte.Equals("aereo") && (transporteDTO.CombustibleTransporte.Equals("fueloleo") || transporteDTO.CombustibleTransporte.Equals("gasoleo")))
            {
                return BadRequest("El transporte aereo solo puede usar el combustible queroseno o gasolinaaviacion");
            }

            Transporte transporte = _mapper.Map<Transporte>(transporteDTO);

            transporte.OrganizacionId = currentUser.OrganizacionId;
            _context.Transporte.Add(transporte);
            await _context.SaveChangesAsync();
            return Ok("Transporte creado correctamente");
        }

        [Authorize(Role.OrgAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporte(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var transporteDelete = await _context.Transporte.FindAsync(id);
                if (currentUser.OrganizacionId != transporteDelete.OrganizacionId)
                {
                    return BadRequest("Este transporte no existe o no pertenece a esta organización");
                }
                _context.Transporte.Remove(transporteDelete);
                await _context.SaveChangesAsync();

                return Ok("Transporte eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún transporte");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Role.SuperAdmin)]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllTransportes()
        {
            List<Transporte> transportelist = await _context.Transporte.ToListAsync();

            foreach (var transporte in transportelist)
            {
                _context.Transporte.Remove(transporte);
            }
            await _context.SaveChangesAsync();

            return Ok("Todos los transportes eliminados correctamente");
        }
    }
}
