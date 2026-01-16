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
        CreateMap<OrdenServicioCreateDto, OrdenServicio>();

        // Caso 2: De la Entidad al DTO (Para leer)
        CreateMap<Vehiculo, VehiculoReadDto>();
        CreateMap<Repuesto, RepuestoReadDto>();
        CreateMap<OrdenServicio, OrdenServicioReadDto>()
            .ForMember(dest => dest.EstadoNombre, // Mapea el nombre del estado desde el enum
                opt => opt.MapFrom(src => src.EstadoActual.ToString())) // Convierte el enum a su nombre como string
            .ForMember(dest => dest.VehiculoInfo, // Mapea la info del vehículo en un solo string
                opt => opt.MapFrom(src => $"{src.Vehiculo.Marca} {src.Vehiculo.Modelo} ({src.Vehiculo.Placa})")); // Formatea la info del vehículo

    }
}