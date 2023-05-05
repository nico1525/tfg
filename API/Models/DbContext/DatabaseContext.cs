using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using API.Models;
using API.Models.Consumos;
using API.Models.Master_Tables;

namespace API.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Organizacion> Organizacion { get; set; } = null!;
        public DbSet<Vehiculo> Vehiculo { get; set; } = null!;
        public DbSet<VehiculoConsumo> VehiculoConsumo { get; set; } = null!;
        public DbSet<FactorEmisionVehiculo> FactorEmisionVehiculo { get; set; } = null!;
        public DbSet<Usuario> Usuario { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Organizacion>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Organizacion>().HasIndex(u => u.NombreOrg).IsUnique();
            builder.Entity<Vehiculo>().HasIndex(u => u.Matricula).IsUnique();
            builder.Entity<FactorEmisionVehiculo>().HasNoKey();
        }

    }
}
