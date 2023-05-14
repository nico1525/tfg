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
        public DbSet<Usuario> Usuario { get; set; } = null!;

        public DbSet<Vehiculo> Vehiculo { get; set; } = null!;
        public DbSet<VehiculoConsumo> VehiculoConsumo { get; set; } = null!;

        public DbSet<Transporte> Transporte { get; set; } = null!;
        public DbSet<TransporteConsumo> TransporteConsumo { get; set; } = null!;

        public DbSet<Maquinaria> Maquinaria { get; set; } = null!;
        public DbSet<MaquinariaConsumo> MaquinariaConsumo { get; set; } = null!;

        public DbSet<EmisionesFugitivas> EmisionesFugitivas { get; set; } = null!;
        public DbSet<EmisionesFugitivasConsumo> EmisionesFugitivasConsumo { get; set; } = null!;

        public DbSet<InstalacionesFijas> InstalacionesFijas { get; set; } = null!;
        public DbSet<InstalacionesFijasConsumo> InstalacionesFijasConsumo { get; set; } = null!;

        public DbSet<OtrosConsumos> OtrosConsumos { get; set; } = null!;





        public DbSet<FactorEmisionVehiculo> FactorEmisionVehiculo { get; set; } = null!;
        public DbSet<FactorEmisionTransporte> FactorEmisionTransporte { get; set; } = null!;
        public DbSet<FactorEmisionesFugitivas> FactorEmisionesFugitivas { get; set; } = null!;
        public DbSet<FactorEmisionMaquinaria> FactorEmisionMaquinaria { get; set; } = null!;
        public DbSet<FactorInstalacionesFijas> FactorInstalacionesFijas { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Organizacion>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Organizacion>().HasIndex(u => u.NombreOrg).IsUnique();
            builder.Entity<Vehiculo>().HasIndex(u => u.Matricula).IsUnique();
            builder.Entity<FactorEmisionVehiculo>().HasNoKey();
            builder.Entity<FactorEmisionTransporte>().HasNoKey();
            builder.Entity<FactorEmisionesFugitivas>().HasNoKey();
            builder.Entity<FactorEmisionMaquinaria>().HasNoKey();
            builder.Entity<FactorInstalacionesFijas>().HasNoKey();
        }

    }
}
