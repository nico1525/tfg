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
    [Route("api/Organizacion/Maquinaria/Consumo")]
    [ApiController]
    public class MaquinariaConsumoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public MaquinariaConsumoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaquinariaConsumoDTO>>> GetMaquinariaConsumo()
        {
            //Devuelve todos los consumos de maquinaria de la empresa
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            var listconsumos = await _context.MaquinariaConsumo.ToListAsync();
            List<MaquinariaConsumoDTO> orgConsumoList = new();
            foreach (var item in listconsumos)
            {
                var maquinaria = await _context.Maquinaria.FindAsync(item.MaquinariaId);

                if (maquinaria.OrganizacionId == currentUser.OrganizacionId)
                {
                    MaquinariaConsumoDTO maquinariaConsumo = _mapper.Map<MaquinariaConsumoDTO>(item);
                    orgConsumoList.Add(maquinariaConsumo);
                }
            }
            return orgConsumoList;
        }

        [HttpGet("{maquinariaid}")]
        public async Task<ActionResult<IEnumerable<MaquinariaConsumoDTO>>> GetMaquinariaConsumo(int id)
        {
            //Devuelve todos los consumos de un maquinaria
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var maquinaria = await _context.Maquinaria.FindAsync(id);
                var listconsumos = await _context.MaquinariaConsumo.ToListAsync();

                if (currentUser.OrganizacionId != maquinaria.OrganizacionId)
                {
                    return BadRequest("Esta maquinaria no pertenece a esta organización");
                }
                List<MaquinariaConsumoDTO> orgConsumoList = new();

                foreach (var item in listconsumos)
                {
                    var maquinariaref = await _context.Maquinaria.FindAsync(item.MaquinariaId);
                    if (maquinariaref.Id == id)
                    {
                        MaquinariaConsumoDTO maquinariaConsumo = _mapper.Map<MaquinariaConsumoDTO>(item);
                        orgConsumoList.Add(maquinariaConsumo);
                    }
                }
                return orgConsumoList;
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ninguna maquinaria");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquinariaConsumo(int id, MaquinariaConsumoModifyDTO maquinariaConsumo)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var maquinariaChange = await _context.MaquinariaConsumo.FindAsync(id);

                var maquinaria = await _context.Maquinaria.FindAsync(maquinariaChange.MaquinariaId);
                DateTime test = new DateTime(1, 1, 1, 0, 0, 0, 0);
                if (currentUser.OrganizacionId != maquinaria.OrganizacionId)
                {
                    return BadRequest("Este consumo de maquinaria no pertenece a esta organización");
                }
                if (maquinariaConsumo.Edificio != null) maquinariaChange.Edificio = maquinariaConsumo.Edificio;
                if (maquinariaConsumo.TipoCombustible != null) maquinariaChange.TipoCombustible = maquinariaConsumo.TipoCombustible;
                if (maquinariaConsumo.CantidadCombustible > 0) maquinariaChange.CantidadCombustible = maquinariaConsumo.CantidadCombustible;
                if (maquinariaConsumo.FechaInicio != test) maquinariaChange.FechaInicio = maquinariaConsumo.FechaInicio;
                if (maquinariaConsumo.FechaFin != test) maquinariaChange.FechaFin = maquinariaConsumo.FechaFin;

                if (DateTime.Compare(maquinariaChange.FechaInicio, maquinariaChange.FechaFin) > 0)
                {
                    return BadRequest("La fecha final debe ser superior a la fecha inicial");
                }
                maquinariaChange.Consumo = CalculoMaquinaria.CalculoConsumoMaquinaria(maquinaria, maquinariaChange, _context);

                await _context.SaveChangesAsync();

                return Ok("Consumo del Maquinaria con Id: " + maquinaria.Id + " modificado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de maquinaria");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MaquinariaConsumo>> PostMaquinariaConsumo(MaquinariaConsumoCreateDTO maquinariaConsumoDTO)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];

            if (DateTime.Compare(maquinariaConsumoDTO.FechaInicio, maquinariaConsumoDTO.FechaFin) > 0)
            {
                return BadRequest("La fecha final debe ser superior a la fecha inicial");
            }
            List<Maquinaria> listmaquinarias = await _context.Maquinaria.ToListAsync();
            foreach (var maquinaria in listmaquinarias)
            {
                if (maquinariaConsumoDTO.MaquinariaId == maquinaria.Id && maquinaria.OrganizacionId == currentUser.OrganizacionId)
                {
                    string? combustible = maquinariaConsumoDTO.TipoCombustible;
                    string? categoria = maquinaria.TipoMaquinaria;
                    
                    if (categoria.Equals("agricola") && (combustible.Equals("E5") || combustible.Equals("E10") || combustible.Equals("E85") || combustible.Equals("E100")))
                    {
                        return BadRequest("Para las maquinarias de tipo agrícola solo están permitidos los combustibles gasoleoB, B7, B10, B20, B30 y B100");
                    }

                    MaquinariaConsumo maquinariaConsumo = _mapper.Map<MaquinariaConsumo>(maquinariaConsumoDTO);

                    maquinariaConsumo.Consumo = CalculoMaquinaria.CalculoConsumoMaquinaria(maquinaria, maquinariaConsumo, _context);
                    maquinariaConsumo.MaquinariaRef = maquinaria;
                    maquinariaConsumo.MaquinariaId = maquinaria.Id;

                    _context.MaquinariaConsumo.Add(maquinariaConsumo);
                    await _context.SaveChangesAsync();
                    return Ok("Consumo para maquinaria con Id: "+ maquinaria.Id +"creado correctamente");
                }

            }
            return BadRequest("No existe una maquinaria con este id registrado en tu empresa");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquinariaConsumo(int id)
        {
            var currentUser = (Usuario)HttpContext.Items["Usuario"];
            try
            {
                var maquinariaDelete = await _context.MaquinariaConsumo.FindAsync(id);
                if (currentUser.OrganizacionId != maquinariaDelete.MaquinariaRef.OrganizacionId)
                {
                    return BadRequest("Este consumo de maquinaria no existe o no pertenece a esta organización");
                }
                _context.MaquinariaConsumo.Remove(maquinariaDelete);
                await _context.SaveChangesAsync();

                return Ok("Consumo de la maquinaria eliminado correctamente");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("El id no corresponde a ningún consumo de maquinaria");
            }
        }
    }
}
