using Microsoft.AspNetCore.Identity;
using SistemaActivosDigitales.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaActivosDigitales.Data
{
    public class DbSeeder
    {
        public static async Task Seed(TallerDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Crear rol Admin si no existe
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Crear usuario Admin si no existe
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var adminUser = new Usuario
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    Nombre = "Admin",
                    Apellido = "Admin",
                    Telefono = "0000000000",
                    Direccion = "Admin Street",
                    ImagenUrlPerfil = "default_admin.png"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (context.Vehiculos.Any() || context.OrdenesServicio.Any() || context.Repuestos.Any() || context.DetalleRepuestos.Any() || context.Users.Any())
            {
                return; // La base de datos ya ha sido sembrada
            }

            // Crear algunos usuarios de prueba
            var user1 = new Usuario
            {
                UserName = "jdoe",
                Email = "jdoe@example.com",
                Nombre = "Juan",
                Apellido = "Doe",
                Telefono = "3001112222",
                Direccion = "Calle 1",
                FechaNacimiento = new DateTime(1990, 1, 1),
                ImagenUrlPerfil = "jdoe.png",
                EmailConfirmed = true
            };

            var user2 = new Usuario
            {
                UserName = "maria",
                Email = "maria@example.com",
                Nombre = "Maria",
                Apellido = "Gomez",
                Telefono = "3003334444",
                Direccion = "Calle 2",
                FechaNacimiento = new DateTime(1985, 5, 20),
                ImagenUrlPerfil = "maria.png",
                EmailConfirmed = true
            };

            var res1 = await userManager.CreateAsync(user1, "User1234!");
            var res2 = await userManager.CreateAsync(user2, "User1234!");

            // Si por alguna razon no se crean los usuarios, lanzar excepción para facilitar debugging
            if (!res1.Succeeded || !res2.Succeeded)
            {
                var errors = string.Join("; ", res1.Errors.Select(e => e.Description).Concat(res2.Errors.Select(e => e.Description)));
                throw new InvalidOperationException($"No se pudieron crear usuarios de prueba: {errors}");
            }

            // Crear vehículos asociados a los usuarios
            var veh1 = new Vehiculo
            {
                Marca = "Toyota",
                Modelo = "Corolla",
                Placa = "ABC123",
                NumeroChasis = "CHASIS001",
                Anio = 2015,
                ImagenVehiculoUrl = "toyota_corolla.png",
                UsuarioId = user1.Id
            };

            var veh2 = new Vehiculo
            {
                Marca = "Ford",
                Modelo = "Fiesta",
                Placa = "XYZ789",
                NumeroChasis = "CHASIS002",
                Anio = 2012,
                ImagenVehiculoUrl = "ford_fiesta.png",
                UsuarioId = user2.Id
            };

            await context.Vehiculos.AddRangeAsync(veh1, veh2);
            await context.SaveChangesAsync();

            // Crear repuestos
            var rep1 = new Repuesto
            {
                NombreRepuesto = "Filtro de aceite",
                CodigoParte = "FP-001",
                PrecioUnitario = 15.50m,
                StockDisponible = 50
            };

            var rep2 = new Repuesto
            {
                NombreRepuesto = "Bujía",
                CodigoParte = "BJ-002",
                PrecioUnitario = 5.25m,
                StockDisponible = 100
            };

            await context.Repuestos.AddRangeAsync(rep1, rep2);
            await context.SaveChangesAsync();

            // Crear órdenes de servicio
            var orden1 = new OrdenServicio
            {
                FechaCreacion = DateTime.Now.AddDays(-10),
                DescripcionFalla = "Ruidos en el motor y pérdida de potencia",
                DiagnosticoTecnico = "Cambio de bujías y filtro de aceite",
                EstadoActual = Estado.Finalizado,
                CostoEstimado = 120.00m,
                VehiculoId = veh1.Id
            };

            var orden2 = new OrdenServicio
            {
                FechaCreacion = DateTime.Now.AddDays(-2),
                DescripcionFalla = "Revisión de frenos",
                DiagnosticoTecnico = "Pastillas de freno desgastadas",
                EstadoActual = Estado.Pendiente,
                CostoEstimado = 80.00m,
                VehiculoId = veh2.Id
            };

            await context.OrdenesServicio.AddRangeAsync(orden1, orden2);
            await context.SaveChangesAsync();

            // Crear detalle de repuestos usados en las órdenes
            var detalle1 = new DetalleRepuesto
            {
                CantidadUtilizada = 4,
                PrecioVenta = rep2.PrecioUnitario,
                OrdenServicioId = orden1.Id,
                RepuestoId = rep2.Id
            };

            var detalle2 = new DetalleRepuesto
            {
                CantidadUtilizada = 1,
                PrecioVenta = rep1.PrecioUnitario,
                OrdenServicioId = orden1.Id,
                RepuestoId = rep1.Id
            };

            await context.DetalleRepuestos.AddRangeAsync(detalle1, detalle2);
            await context.SaveChangesAsync();

        }
    }

}
