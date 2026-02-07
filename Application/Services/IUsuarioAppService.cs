using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public interface IUsuarioAppService
    {
        Task<IEnumerable<UsuarioDto>> ListarTodosAsync();
        Task<UsuarioDto?> BuscarPorIdAsync(Guid id);
        Task<UsuarioDto?> BuscarPorCodigoAsync(int codigo);
        Task<UsuarioDto?> BuscarPorEmailAsync(string email);
        Task<UsuarioDto> AdicionarAsync(CriarUsuarioDto dto);
        Task<UsuarioDto> AtualizarAsync(Guid id, AtualizarUsuarioDto dto);
        Task RemoverAsync(Guid id);
    }
}
