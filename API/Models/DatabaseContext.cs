using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<TestItem> ItemTest { get; set; } = null!;
        public DbSet<Organizacion> Organizacion { get; set; } = null!;

    }
}
