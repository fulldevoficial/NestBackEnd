using Application.DTOs;

namespace Application.Ports.Input
{
    public interface IUsuarioUseCase
    {
        Task<IEnumerable<UsuarioDto>> ListarTodosAsync();
        Task<UsuarioDto?> BuscarPorIdAsync(int id);
        Task<(bool sucesso, string mensagem)> CriarAsync(CriarUsuarioDto dto);
        Task<(bool sucesso, string mensagem)> AtualizarAsync(int id, AtualizarUsuarioDto dto);
        Task<(bool sucesso, string mensagem)> RemoverAsync(int id);
    }
}
