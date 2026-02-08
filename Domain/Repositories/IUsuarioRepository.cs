using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> BuscarPorCodigoAsync(int codigo);
        Task<Usuario?> BuscarPorEmailAsync(string email);
    }
}
