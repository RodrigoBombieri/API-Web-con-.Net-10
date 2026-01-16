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
        public async Task<ActionResult<IEnumerable<VehiculoReadDto>>> GetVehiculos()
        {
            var vehiculos = await _context.Vehiculos.ToListAsync();
            // Esto es solo para probar que el navegador recibe datos
            // Devuelve la lista de Vehiculo mapeada a una lista de VehiculoReadDto
            // Es decir devuelve un VehiculoReadDto por cada Vehiculo en la lista
            // Uso IEnumerable en lugar de list para mayor flexibilidad
            // Por ejemplo, IEnumerable permite devolver diferentes tipos de colecciones
            return Ok(_mapper.Map<IEnumerable<VehiculoReadDto>>(vehiculos));
        }

        // GET: api/Vehiculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoReadDto>> GetVehiculo(int id)
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
        public async Task<IActionResult> PutVehiculo(int id, VehiculoCreateDto dto)
        {
            var vehiculoExistente = await _context.Vehiculos.FindAsync(id);
            if (vehiculoExistente == null) return NotFound();

            // Mapeo: vuelca los datos del DTO sobre la entidad que ya existe en la DB
            // La funcion Map asigna los valores de las propiedades del dto
            // a las propiedades correspondientes de la entidad
            _mapper.Map(dto, vehiculoExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Vehiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehiculoReadDto>> PostVehiculo(VehiculoCreateDto dto)
        {
            // 1. Mapeo de DTO -> Entidad
            var vehiculo = _mapper.Map<Vehiculo>(dto);

            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            var lecturaDto = _mapper.Map<VehiculoReadDto>(vehiculo);
            // CreatedAtAction: Devuelve un código 201 junto con la ubicación del nuevo recurso
            // El primer parámetro es el nombre del método GET para obtener este recurso individual
            // El segundo parámetro es un objeto anónimo que contiene los parámetros de ruta necesarios (en este caso, el id)
            // El tercer parámetro es el DTO que se devolverá en el cuerpo de la respuesta
            return CreatedAtAction(nameof(GetVehiculos), new { id = vehiculo.Id }, lecturaDto);
        }

        // DELETE: api/Vehiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null) return NotFound();

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
