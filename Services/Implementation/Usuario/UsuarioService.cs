using Data;
using Microsoft.EntityFrameworkCore;
using Models.Usuario;
using Services.Interfaces.Usuario;

namespace Services.Implementation.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioModel>> Listar()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<UsuarioModel?> BuscarPorId(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<(bool sucesso, string mensagem)> Criar(UsuarioModel usuario)
        {
            try
            {
                usuario.PASSWORD = BCrypt.Net.BCrypt.HashPassword(usuario.PASSWORD);

                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();

                return (true, "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(int id, UsuarioModel usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(id);
            if (usuarioExistente == null)
                return (false, "Usuário não encontrado.");

            usuarioExistente.NOME = usuario.NOME;
            usuarioExistente.EMAIL = usuario.EMAIL;
            usuarioExistente.PASSWORD = usuario.PASSWORD;

            try
            {
                _context.Usuario.Update(usuarioExistente);
                await _context.SaveChangesAsync();
                return (true, "Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Remover(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return (false, "Usuário não encontrado.");

            try
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
                return (true, "Usuário removido com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao remover usuário: {ex.Message}");
            }
        }

        public async Task<UsuarioModel?> BuscarPorEmail(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.EMAIL == email);
        }

    }
}
