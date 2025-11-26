using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Usuario
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioModel>> Listar();
        Task<UsuarioModel?> BuscarPorId(int id);
        Task<(bool sucesso, string mensagem)> Criar(UsuarioModel usuario);
        Task<(bool sucesso, string mensagem)> Atualizar(int id, UsuarioModel usuario);
        Task<(bool sucesso, string mensagem)> Remover(int id);
        Task<UsuarioModel?> BuscarPorEmail(string email);
    }
}
