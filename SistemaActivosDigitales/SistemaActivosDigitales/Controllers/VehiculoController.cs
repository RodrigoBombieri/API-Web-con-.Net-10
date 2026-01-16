using AutoMapper;
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
    public class VehiculoController : ControllerBase
    {
        private readonly TallerDbContext _context;
        private readonly IMapper _mapper;
        public VehiculoController(TallerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        {
            var vehiculos = await _context.Vehiculos.ToListAsync();
            // Esto es solo para probar que el navegador recibe datos
            return Ok(_mapper.Map<List<VehiculoReadDto>>(vehiculos));
        }

        // GET: api/Vehiculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }
            // Mapeo de Entidad -> DTO
            return Ok(_mapper.Map<VehiculoReadDto>(vehiculo));
        }

        // PUT: api/Vehiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(VehiculoCreateDto dto)
        {
            // 1. Mapeo de DTO -> Entidad
            var vehiculo = _mapper.Map<Vehiculo>(dto);

            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            var lecturaDto = _mapper.Map<VehiculoReadDto>(vehiculo);
            // CreatedAtAction: Devuelve un código 201 junto con la ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetVehiculos), new { id = vehiculo.Id }, lecturaDto);
        }

        // DELETE: api/Vehiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.Id == id);
        }
    }
}
