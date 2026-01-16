using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaActivosDigitales.Data;
using SistemaActivosDigitales.DTOs;
using SistemaActivosDigitales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaActivosDigitales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestoController : ControllerBase
    {
        private readonly TallerDbContext _context;
        private readonly IMapper _mapper;

        public RepuestoController(TallerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Repuesto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepuestoReadDto>>> GetRepuestos()
        {
            // Obtener la lista completa de repuestos desde la base de datos
            var repuestos = await _context.Repuestos.ToListAsync();

            // AutoMapper convierte toda la lista en una lista de DTOs
            // para poder devolver solo los campos necesarios
            return Ok(_mapper.Map<IEnumerable<RepuestoReadDto>>(repuestos));
        }

        // GET: api/Repuesto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repuesto>> GetRepuesto(int id)
        {
            var repuesto = await _context.Repuestos.FindAsync(id);

            if (repuesto == null)
            {
                return NotFound();
            }
            // Devolvemos el DTO en lugar de la entidad directamente
            return Ok(_mapper.Map<RepuestoReadDto>(repuesto));
        }

        // PUT: api/Repuesto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepuesto(int id, RepuestoCreateDto dto)
        {
            // 1. Buscar la entidad existente en la base de datos
            var repuestoExistente = await _context.Repuestos.FindAsync(id);

            if (repuestoExistente == null)
            {
                return NotFound();
            }

            // Mapeo manual: actualizas campo por campo
            repuestoExistente.NombreRepuesto = dto.NombreRepuesto;
            repuestoExistente.CodigoParte = dto.CodigoParte;
            repuestoExistente.PrecioUnitario = dto.PrecioUnitario;
            repuestoExistente.StockDisponible = dto.StockDisponible;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepuestoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Repuesto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RepuestoReadDto>> PostRepuesto(RepuestoCreateDto dto)
        {
            // Transformamos el DTO para crear una nueva entidad a un objeto Repuesto
            // y ahi si puede hacer el mapeo automático e ir a la base de datos
            var repuesto = _mapper.Map<Repuesto>(dto);
            // Añadimos la entidad al contexto y guardamos
            _context.Repuestos.Add(repuesto);
            await _context.SaveChangesAsync();
            // Transformamos la entidad guardada a un DTO de lectura para la respuesta
            // (esto es útil si la entidad tiene campos generados como Id)
            var lecturaDto = _mapper.Map<RepuestoReadDto>(repuesto);
            // Devolvemos un 201 Created con la ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetRepuestos), new { id = repuesto.Id }, lecturaDto);
        }

        // DELETE: api/Repuesto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepuesto(int id)
        {
            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }

            _context.Repuestos.Remove(repuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepuestoExists(int id)
        {
            return _context.Repuestos.Any(e => e.Id == id);
        }
    }
}
