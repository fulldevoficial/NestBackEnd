using Data;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Models.Usuario;
using Services.Interfaces.Usuario;

namespace Services.Implementation.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;
        private readonly IIdentityGenerator _identityGenerator;

        public UsuarioService(AppDbContext context, IIdentityGenerator identityGenerator)
        {
            _context = context;
            _identityGenerator = identityGenerator;
        }

        public async Task<IEnumerable<UsuarioModel>> Listar()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<UsuarioModel?> BuscarPorId(Guid id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<UsuarioModel?> BuscarPorCodigo(int codigo)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.CodigoUsuario == codigo);
        }

        public async Task<(bool sucesso, string mensagem)> Criar(UsuarioModel usuario)
        {
            try
            {
                // Gera um novo UUIDv7 para o usuário
                usuario.Id = _identityGenerator.Generate();

                // Hash da senha
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();

                return (true, "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(Guid id, UsuarioModel usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(id);
            if (usuarioExistente == null)
                return (false, "Usuário não encontrado.");

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;

            // Só atualiza a senha se foi informada
            if (!string.IsNullOrWhiteSpace(usuario.Password))
            {
                usuarioExistente.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            }

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

        public async Task<(bool sucesso, string mensagem)> Remover(Guid id)
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
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
