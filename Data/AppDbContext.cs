using Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Models.Usuario;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica as configurações usando Fluent API
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
