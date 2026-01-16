namespace SistemaActivosDigitales.DTOs
{
    public class RepuestoCreateDto
    {
        public string NombreRepuesto { get; set; } = string.Empty;
        public string CodigoParte { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }

    }
}
