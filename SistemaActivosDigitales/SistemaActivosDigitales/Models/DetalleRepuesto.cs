using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace SistemaActivosDigitales.Models
{
    public class DetalleRepuesto
    {
        public int Id { get; set; }
        [Required]
        public int CantidadUtilizada { get; set; }
        [Required]
        public decimal PrecioVenta { get; set; }
        public int OrdenServicioId { get; set; }
        public OrdenServicio? OrdenServicio { get; set; }
        public int RepuestoId { get; set; }
        public Repuesto? Repuesto { get; set; }
    }
}
