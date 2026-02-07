using Models.Usuario;

namespace Services.Interfaces.Usuario
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioModel>> Listar();
        Task<UsuarioModel?> BuscarPorId(Guid id);
        Task<UsuarioModel?> BuscarPorCodigo(int codigo);
        Task<(bool sucesso, string mensagem)> Criar(UsuarioModel usuario);
        Task<(bool sucesso, string mensagem)> Atualizar(Guid id, UsuarioModel usuario);
        Task<(bool sucesso, string mensagem)> Remover(Guid id);
        Task<UsuarioModel?> BuscarPorEmail(string email);
    }
}
