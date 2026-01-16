namespace SistemaActivosDigitales.DTOs
{
    public class OrdenServicioReadDto
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string DescripcionFalla { get; set; } = string.Empty;
        public string DiagnosticoTecnico { get; set; } = string.Empty;
        public string EstadoNombre { get; set; } = string.Empty; // "Pendiente", "En Proceso", etc.
        public decimal CostoEstimado { get; set; }

        // Datos simplificados del vehículo vinculado
        public int VehiculoId { get; set; }
        public string VehiculoInfo { get; set; } = string.Empty; // Ej: "Toyota Corolla (ABC-123)"
    }
}
