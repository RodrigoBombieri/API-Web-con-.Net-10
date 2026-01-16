namespace SistemaActivosDigitales.DTOs
{
    public class OrdenServicioCreateDto
    {
        public string DescripcionFalla { get; set; } = string.Empty;
        public string DiagnosticoTecnico { get; set; } = string.Empty;
        public int EstadoActual { get; set; } // Lo recibimos como int del Enum
        public decimal CostoEstimado { get; set; }
        public int VehiculoId { get; set; }
    }
}
