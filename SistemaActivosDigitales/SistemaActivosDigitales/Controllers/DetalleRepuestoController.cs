using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
    public class DetalleRepuestoController : ControllerBase
    {
        private readonly TallerDbContext _context;
        private readonly IMapper _mapper;

        public DetalleRepuestoController(TallerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DetalleRepuesto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRepuestoReadDto>>> GetDetalleRepuestos()
        {
            var detalleRepuestos = await _context.DetalleRepuestos.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<DetalleRepuestoReadDto>>(detalleRepuestos));
        }

        // GET: api/DetalleRepuesto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleRepuestoReadDto>> GetDetalleRepuesto(int id)
        {
            var detalleRepuesto = await _context.DetalleRepuestos.FindAsync(id);

            if (detalleRepuesto == null) return NotFound();

            return Ok(_mapper.Map<DetalleRepuestoReadDto>(detalleRepuesto));
        }

        // PUT: api/DetalleRepuesto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleRepuesto(int id, DetalleRepuestoCreateDto dto)
        {
            var detalleRepuestoExistente = _context.DetalleRepuestos.FindAsync(id);
            if (detalleRepuestoExistente == null) return NotFound();
            
            _mapper.Map(dto, detalleRepuestoExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleRepuestoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/DetalleRepuesto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleRepuestoReadDto>> PostDetalleRepuesto(DetalleRepuestoCreateDto dto)
        {
            // 1. Verificar si el repuesto existe y obtener su información
            var repuesto = await _context.Repuestos.FindAsync(dto.RepuestoId);
            if (repuesto == null) return NotFound("El repuesto no existe.");
            // 2. Verificar si hay suficiente stock disponible
            if (repuesto.StockDisponible < dto.CantidadUtilizada)
            {
                return BadRequest($"Stock insuficiente. Disponible: {repuesto.StockDisponible}");
            }
            // 3. Crear el detalle del repuesto
            var detalleRepuesto = _mapper.Map<DetalleRepuesto>(dto);
            detalleRepuesto.PrecioVenta = repuesto.PrecioUnitario;
            // 4. Actualizar el stock disponible del repuesto
            repuesto.StockDisponible -= dto.CantidadUtilizada;
            _context.DetalleRepuestos.Add(detalleRepuesto);

            await _context.SaveChangesAsync(); // Nos aseguramos de guardar AMBOS cambios
            // 5. Cargar el nombre del repuesto para el DTO de respuesta
            await _context.Entry(detalleRepuesto).Reference(d => d.Repuesto).LoadAsync();
            // 6. Mapear a DetalleRepuestoReadDto
            var lecturaDto = _mapper.Map<DetalleRepuestoReadDto>(detalleRepuesto);
            // 7. Retornar el DTO con el subtotal calculado
            return Ok(lecturaDto);
        }

        // DELETE: api/DetalleRepuesto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleRepuesto(int id)
        {
            var detalleRepuesto = await _context.DetalleRepuestos.FindAsync(id);
            if (detalleRepuesto == null)
            {
                return NotFound();
            }

            _context.DetalleRepuestos.Remove(detalleRepuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleRepuestoExists(int id)
        {
            return _context.DetalleRepuestos.Any(e => e.Id == id);
        }
    }
}
