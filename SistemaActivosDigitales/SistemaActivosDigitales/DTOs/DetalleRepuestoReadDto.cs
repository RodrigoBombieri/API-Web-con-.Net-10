namespace SistemaActivosDigitales.DTOs
{
    public class DetalleRepuestoReadDto
    {
        public int Id { get; set; }
        public int CantidadUtilizada { get; set; }
        public decimal PrecioVenta { get; set; }
        public string NombreRepuesto { get; set; } = string.Empty;
        public decimal Subtotal => CantidadUtilizada * PrecioVenta;
    }
}
