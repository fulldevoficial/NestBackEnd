using Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Usuario;

namespace Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            var usuariosModel = await _context.Usuario.ToListAsync();
            return usuariosModel.Select(MapToDomain);
        }

        public async Task<Usuario?> BuscarPorIdAsync(Guid id)
        {
            var usuarioModel = await _context.Usuario.FindAsync(id);
            return usuarioModel != null ? MapToDomain(usuarioModel) : null;
        }

        public async Task<Usuario?> BuscarPorCodigoAsync(int codigo)
        {
            var usuarioModel = await _context.Usuario
                .FirstOrDefaultAsync(u => u.CodigoUsuario == codigo);
            return usuarioModel != null ? MapToDomain(usuarioModel) : null;
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            var usuarioModel = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == email);
            return usuarioModel != null ? MapToDomain(usuarioModel) : null;
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            var usuarioModel = MapToModel(usuario);
            _context.Usuario.Add(usuarioModel);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            var usuarioModel = await _context.Usuario.FindAsync(usuario.Id);
            if (usuarioModel == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            usuarioModel.CodigoUsuario = usuario.CodigoUsuario;
            usuarioModel.Nome = usuario.Nome;
            usuarioModel.Email = usuario.Email;
            usuarioModel.Password = usuario.PasswordHash;

            _context.Usuario.Update(usuarioModel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Usuario usuario)
        {
            var usuarioModel = await _context.Usuario.FindAsync(usuario.Id);
            if (usuarioModel == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            _context.Usuario.Remove(usuarioModel);
            await _context.SaveChangesAsync();
        }

        // Mappers
        private static Usuario MapToDomain(UsuarioModel model)
        {
            return new Usuario(
                model.Id,
                model.CodigoUsuario,
                model.Nome,
                model.Email,
                model.Password
            );
        }

        private static UsuarioModel MapToModel(Usuario usuario)
        {
            return new UsuarioModel
            {
                Id = usuario.Id,
                CodigoUsuario = usuario.CodigoUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Password = usuario.PasswordHash
            };
        }
    }
}
