using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaActivosDigitales.Data;
using SistemaActivosDigitales.DTOs;
using SistemaActivosDigitales.Models;

namespace SistemaActivosDigitales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly TallerDbContext _context;
        private readonly IMapper _mapper;

        public OrdenServicioController(TallerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrdenServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenServicioReadDto>>> GetOrdenes()
        {
            var ordenes = await _context.OrdenesServicio
                .Include(o => o.Vehiculo)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<OrdenServicioReadDto>>(ordenes));
        }

        // GET: api/OrdenServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenServicioReadDto>> GetOrden(int id)
        {
            var ordenServicio = await _context.OrdenesServicio.FindAsync(id);

            if (ordenServicio == null) return NotFound();

            return Ok(_mapper.Map<OrdenServicioReadDto>(ordenServicio));
        }

        // PUT: api/OrdenServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenServicio(int id, OrdenServicioCreateDto dto)
        {
            var ordenServicioExistente = await _context.OrdenesServicio.FindAsync(id);

            if (ordenServicioExistente == null) return NotFound();

            _mapper.Map(dto, ordenServicioExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenServicioExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/OrdenServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenServicioReadDto>> PostOrdenServicio(OrdenServicioCreateDto dto)
        {
            // 1. Mapear el DTO a la entidad OrdenServicio
            var ordenServicio = _mapper.Map<OrdenServicio>(dto);
            ordenServicio.FechaCreacion = DateTime.Now; // Asegurar que la fecha de creación se establezca al momento actual
            // 2. Agregar la nueva orden de servicio al contexto
            _context.OrdenesServicio.Add(ordenServicio);
            await _context.SaveChangesAsync();
            // 3. Volvemos a cargar con el Vehiculo para que el ReadDto salga completo
            await _context.Entry(ordenServicio).Reference(o => o.Vehiculo).LoadAsync(); // Cargar la referencia del vehículo
            // 4. Mapear la entidad creada de vuelta a un DTO de lectura
            var lecturaDto = _mapper.Map<OrdenServicioReadDto>(ordenServicio);
            // 5. Retornar la respuesta con el DTO de lectura
            return CreatedAtAction(nameof(GetOrdenes), new { id = ordenServicio.Id }, lecturaDto);
        }

        // DELETE: api/OrdenServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenServicio(int id)
        {
            var ordenServicio = await _context.OrdenesServicio.FindAsync(id);
            if (ordenServicio == null)
            {
                return NotFound();
            }

            _context.OrdenesServicio.Remove(ordenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenServicioExists(int id)
        {
            return _context.OrdenesServicio.Any(e => e.Id == id);
        }
    }
}
