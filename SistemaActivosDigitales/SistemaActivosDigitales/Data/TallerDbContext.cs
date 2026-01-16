using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaActivosDigitales.Models;

namespace SistemaActivosDigitales.Data
{
    public class TallerDbContext : IdentityDbContext<Usuario>
    {
        public TallerDbContext(DbContextOptions<TallerDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<OrdenServicio> OrdenesServicio { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<DetalleRepuesto> DetalleRepuestos { get; set; }
    }
}
