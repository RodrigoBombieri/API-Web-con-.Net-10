using AutoMapper;
using SistemaActivosDigitales.DTOs;
using SistemaActivosDigitales.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Caso 1: Del DTO a la Entidad (Para crear/editar)
        CreateMap<VehiculoCreateDto, Vehiculo>();
        CreateMap<RepuestoCreateDto, Repuesto>();

        // Caso 2: De la Entidad al DTO (Para leer)
        CreateMap<Vehiculo, VehiculoReadDto>();
        CreateMap<Repuesto, RepuestoReadDto>();


    }
}