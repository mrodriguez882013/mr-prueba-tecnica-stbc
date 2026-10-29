using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Entities;

namespace PruebaTecnica.Api.Data
{
    public class PruebaTecnicaDbContext : DbContext
    {
        public PruebaTecnicaDbContext(DbContextOptions<PruebaTecnicaDbContext> options) : base(options) { }

        // DbSet para cada una de las tablas del sistema
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!;
        public DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //---------------------------
            // Fluent API Configuration
            //---------------------------

            // 1. Relación Ventas -> DetalleVentas (Borrado en cascada configurado en BD y EF)
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Relación Productos -> DetalleVentas (Restringir borrado de producto si tiene ventas asociadas)
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.DetalleVentas)
                .HasForeignKey(d => d.IdPro)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. Relación Usuarios -> Productos (Auditoría de creación de productos)
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.UsuarioCreacion)
                .WithMany(u => u.ProductosCreados)
                .HasForeignKey(p => p.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Relación Usuarios -> Ventas (Auditoría de registro de ventas)
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.UsuarioCreacion)
                .WithMany(u => u.VentasCreadas)
                .HasForeignKey(v => v.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}