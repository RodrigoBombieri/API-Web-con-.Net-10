using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SistemaActivosDigitales.Models
{
    
    public class Vehiculo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        public string Placa { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroChasis { get; set; }
        [Range(1900, 2100)]
        public int Anio { get; set; }
        public string ImagenVehiculoUrl { get; set; }
        // Identity uses string keys by default, make UsuarioId a string to match Usuario.Id
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<OrdenServicio>? OrdenesServicio { get; set; }
    }
}
