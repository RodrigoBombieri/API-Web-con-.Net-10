namespace SistemaActivosDigitales.DTOs
{
    public class VehiculoCreateDto
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public int Anio { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
    }
}
