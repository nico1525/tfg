using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace API.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Organizacion> Organizacion { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Organizacion>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Organizacion>().HasIndex(u => u.NombreOrg).IsUnique();
        }
    }

}
