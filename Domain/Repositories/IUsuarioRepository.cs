using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ListarTodosAsync();
        Task<Usuario?> BuscarPorIdAsync(int id);
        Task<Usuario?> BuscarPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task RemoverAsync(Usuario usuario);
    }
}
