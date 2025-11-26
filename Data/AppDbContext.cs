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
    }
}
