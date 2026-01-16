using System.ComponentModel.DataAnnotations;

namespace SistemaActivosDigitales.Models
{
    public enum Estado { Pendiente, EnProceso, Finalizado, Entregado }
    public class OrdenServicio
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [StringLength(1000)]
        public string DescripcionFalla { get; set; }
        [Required]
        [StringLength(500)]
        public string DiagnosticoTecnico { get; set; }
        public Estado EstadoActual { get; set; }
        public decimal CostoEstimado { get; set; }
        public int VehiculoId { get; set; }
        public Vehiculo? Vehiculo { get; set; }
        public List<DetalleRepuesto>? DetalleRepuestos { get; set; }
    }
}
