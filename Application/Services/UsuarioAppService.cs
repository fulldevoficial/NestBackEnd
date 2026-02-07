using Application.DTOs;
using Application.Ports.Output;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioAppService(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UsuarioDto>> ListarTodosAsync()
        {
            var usuarios = await _usuarioRepository.ListarTodosAsync();
            return usuarios.Select(MapToDto);
        }

        public async Task<UsuarioDto?> BuscarPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
            return usuario != null ? MapToDto(usuario) : null;
        }

        public async Task<UsuarioDto?> BuscarPorCodigoAsync(int codigo)
        {
            var usuario = await _usuarioRepository.BuscarPorCodigoAsync(codigo);
            return usuario != null ? MapToDto(usuario) : null;
        }

        public async Task<UsuarioDto?> BuscarPorEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.BuscarPorEmailAsync(email);
            return usuario != null ? MapToDto(usuario) : null;
        }

        public async Task<UsuarioDto> AdicionarAsync(CriarUsuarioDto dto)
        {
            var passwordHash = _passwordHasher.HashPassword(dto.Password);
            var usuario = new Usuario(Guid.NewGuid(), dto.CodigoUsuario, dto.Nome, dto.Email, passwordHash);
            
            await _usuarioRepository.AdicionarAsync(usuario);
            
            return MapToDto(usuario);
        }

        public async Task<UsuarioDto> AtualizarAsync(Guid id, AtualizarUsuarioDto dto)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            usuario.AtualizarDados(dto.Nome, dto.Email);

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = _passwordHasher.HashPassword(dto.Password);
                usuario.AtualizarSenha(passwordHash);
            }

            await _usuarioRepository.AtualizarAsync(usuario);

            return MapToDto(usuario);
        }

        public async Task RemoverAsync(Guid id)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            await _usuarioRepository.RemoverAsync(usuario);
        }

        private static UsuarioDto MapToDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.Id,
                CodigoUsuario = usuario.CodigoUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }
    }
}
