using Application.DTOs;

namespace Application.Ports.Input
{
    public interface IUsuarioUseCase
    {
        Task<IEnumerable<UsuarioDto>> ListarTodosAsync();
        Task<UsuarioDto?> BuscarPorIdAsync(Guid id);
        Task<UsuarioDto?> BuscarPorCodigoAsync(int codigo);
        Task<(bool sucesso, string mensagem, Guid? id)> CriarAsync(CriarUsuarioDto dto);
        Task<(bool sucesso, string mensagem)> AtualizarAsync(Guid id, AtualizarUsuarioDto dto);
        Task<(bool sucesso, string mensagem)> RemoverAsync(Guid id);
    }
}
