using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using API.Models;
using API.Models.Consumos;

namespace API.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Organizacion> Organizacion { get; set; } = null!;
        public DbSet<Dispositivo> Dispositivo { get; set; } = null!;
        public DbSet<Vehiculo> Vehiculo { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Organizacion>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Organizacion>().HasIndex(u => u.NombreOrg).IsUnique();
            builder.Entity<Vehiculo>().HasIndex(u => u.Matricula).IsUnique();
        }



        public DbSet<API.Models.Consumos.VehiculoConsumo> VehiculoConsumo { get; set; } = default!;

    }
}
