using System.ComponentModel.DataAnnotations;

namespace SistemaActivosDigitales.Models
{
    public class Repuesto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreRepuesto { get; set; }
        [Required]
        [StringLength(50)]
        public string CodigoParte { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public List<DetalleRepuesto>? DetalleRepuestos { get; set; }
    }
}
