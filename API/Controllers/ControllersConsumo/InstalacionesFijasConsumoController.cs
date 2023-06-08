using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    [Route("api/Organizacion/InstalacionesFijas/Consumo")]
    [ApiController]
    public class InstalacionesFijasConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public InstalacionesFijasConsumoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstalacionesFijasConsumoDTO>>> GetInstalacionesFijasConsumo()
        {
            //Devuelve todos los consumos de instalacionesFija de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.InstalacionesFijasConsumo.ToListAsync();
            List<InstalacionesFijasConsumoDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                var instalacionesFija = await _context.InstalacionesFijas.FindAsync(item.InstalacionesFijasId);

                if (instalacionesFija.OrganizacionId == currentUser.OrganizacionId)
                {
                    InstalacionesFijasConsumoDTO instalacionesFijaConsumo = _mapper.Map<InstalacionesFijasConsumoDTO>(item);
                    orgConsumoList.Add(instalacionesFijaConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<InstalacionesFijasConsumoDTO>> GetInstalacionesFijasConsumoId(int id)
        {
            //Devuelve el consumos por su id
            var consumo = await _context.InstalacionesFijasConsumo.FindAsync(id);
            InstalacionesFijasConsumoDTO consumodto = _mapper.Map<InstalacionesFijasConsumoDTO>(consumo);

            return consumodto;
        }

        [HttpGet("{instalacionesFijaid}")]
        public async Task<ActionResult<IEnumerable<InstalacionesFijasConsumoDTO>>> GetInstalacionesFijasConsumo(int id)
        {
            //Devuelve todos los consumos de un instalacionesFija
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var instalacionesFija = await _context.InstalacionesFijas.FindAsync(id);
                var listconsumos = await _context.InstalacionesFijasConsumo.ToListAsync();

                if (currentUser.OrganizacionId != instalacionesFija.OrganizacionId)
                {
                    return BadRequest("Esta instalación fija no pertenece a esta organización");
                }
                List<InstalacionesFijasConsumoDTO> orgConsumoList = new();

                foreach (var item in listconsumos)
                {
                    var instalacionesFijaref = await _context.InstalacionesFijas.FindAsync(item.InstalacionesFijasId);
                    if (instalacionesFijaref.Id == id)
                    {
                        InstalacionesFijasConsumoDTO instalacionesFijaConsumo = _mapper.Map<InstalacionesFijasConsumoDTO>(item);
                        orgConsumoList.Add(instalacionesFijaConsumo);
                    }
                }
                return orgConsumoList;
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ninguna instalación fija");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstalacionesFijasConsumo(int id, InstalacionesFijasConsumoModifyDTO instalacionesFijaConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var instalacionesFijaChange = await _context.InstalacionesFijasConsumo.FindAsync(id);

                var instalacionesFija = await _context.InstalacionesFijas.FindAsync(instalacionesFijaChange.InstalacionesFijasId);
                DateTime test = new(1, 1, 1, 0, 0, 0, 0);
                if (currentUser.OrganizacionId != instalacionesFija.OrganizacionId)
                {
                    return BadRequest("Este consumo de instalación fija no pertenece a esta organización");
                }
                if (instalacionesFijaConsumo.Edificio != null && instalacionesFijaConsumo.Edificio != "") instalacionesFijaChange.Edificio = instalacionesFijaConsumo.Edificio;
                if (instalacionesFijaConsumo.TipoCombustible != null && instalacionesFijaConsumo.TipoCombustible != "") instalacionesFijaChange.TipoCombustible = instalacionesFijaConsumo.TipoCombustible;
                if (instalacionesFijaConsumo.CantidadCombustible > 0) instalacionesFijaChange.CantidadCombustible = instalacionesFijaConsumo.CantidadCombustible;
                if (instalacionesFijaConsumo.FechaInicio != test) instalacionesFijaChange.FechaInicio = instalacionesFijaConsumo.FechaInicio;
                if (instalacionesFijaConsumo.FechaFin != test) instalacionesFijaChange.FechaFin = instalacionesFijaConsumo.FechaFin;

                if (DateTime.Compare(instalacionesFijaChange.FechaInicio, instalacionesFijaChange.FechaFin) > 0)
                {
                    return BadRequest("La fecha final debe ser superior a la fecha inicial");
                }

                try
                {
                    instalacionesFijaChange.Consumo = CalculoInstalacionesFijas.CalculoConsumoInstalacionesFijas(instalacionesFijaChange, _context);
                }
                catch (NullReferenceException)
                {
                    return BadRequest("Este Tipo de combustible no es válido");
                }
                await _context.SaveChangesAsync();

                return Ok("Consumo de instalación fija con Id: " + instalacionesFija.Id + " modificado correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún consumo de instalación fija");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InstalacionesFijasConsumo>> PostInstalacionesFijasConsumo(InstalacionesFijasConsumoCreateDTO instalacionesFijaConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(instalacionesFijaConsumoDTO.FechaInicio, instalacionesFijaConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            List<InstalacionesFijas> listinstalacionesFijas = await _context.InstalacionesFijas.ToListAsync();
            foreach (var instalacionesFija in listinstalacionesFijas)
            {
                if (instalacionesFijaConsumoDTO.InstalacionesFijasId == instalacionesFija.Id && instalacionesFija.OrganizacionId == currentUser.OrganizacionId)
                {
                    InstalacionesFijasConsumo instalacionesFijaConsumo = _mapper.Map<InstalacionesFijasConsumo>(instalacionesFijaConsumoDTO);
                    try { 
                        instalacionesFijaConsumo.Consumo = CalculoInstalacionesFijas.CalculoConsumoInstalacionesFijas(instalacionesFijaConsumo, _context);
                    }
                    catch (NullReferenceException ex)
                    {
                        return BadRequest("Este Tipo de combustible no es válido");
                    }
                    instalacionesFijaConsumo.InstalacionesFijasId = instalacionesFija.Id;

                    _context.InstalacionesFijasConsumo.Add(instalacionesFijaConsumo);
                    await _context.SaveChangesAsync();
                    return Ok("Consumo para instalación fija con Id: " + instalacionesFija.Id + " creado correctamente");
                }

            }
            return BadRequest("No existe una instalación fija con este id registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalacionesFijasConsumo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {  
                var instalacionesFijaDelete = await _context.InstalacionesFijasConsumo.FindAsync(id);
                var InstalacionesFijas = await _context.InstalacionesFijas.FindAsync(instalacionesFijaDelete.InstalacionesFijasId);

                if (currentUser.OrganizacionId != InstalacionesFijas.OrganizacionId)
                {
                    return BadRequest("Este consumo de instalación fija no existe o no pertenece a esta organización");
                }
                _context.InstalacionesFijasConsumo.Remove(instalacionesFijaDelete);
                await _context.SaveChangesAsync();

                return Ok("Consumo de la instalación fija eliminado correctamente");
            }
            catch (NullReferenceException)
            {
                return BadRequest("El id no corresponde a ningún consumo de instalación fija");
            }
        }
    }
}
