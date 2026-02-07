using Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> BuscarPorCodigoAsync(int codigo)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.CodigoUsuario == codigo);
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
