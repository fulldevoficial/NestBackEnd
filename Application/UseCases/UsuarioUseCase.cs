using Application.DTOs;
using Application.Ports.Input;
using Application.Ports.Output;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioUseCase(IUsuarioRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UsuarioDto>> ListarTodosAsync()
        {
            var usuarios = await _repository.ListarTodosAsync();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            });
        }

        public async Task<UsuarioDto?> BuscarPorIdAsync(int id)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<(bool sucesso, string mensagem)> CriarAsync(CriarUsuarioDto dto)
        {
            try
            {
                var passwordHash = _passwordHasher.HashPassword(dto.Password);
                var usuario = new Usuario(dto.Nome, dto.Email, passwordHash);

                await _repository.AdicionarAsync(usuario);
                return (true, "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarAsync(int id, AtualizarUsuarioDto dto)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);
            if (usuario == null)
                return (false, "Usuário não encontrado.");

            try
            {
                usuario.AtualizarDados(dto.Nome, dto.Email);
                
                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    var passwordHash = _passwordHasher.HashPassword(dto.Password);
                    usuario.AtualizarSenha(passwordHash);
                }

                await _repository.AtualizarAsync(usuario);
                return (true, "Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> RemoverAsync(int id)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);
            if (usuario == null)
                return (false, "Usuário não encontrado.");

            try
            {
                await _repository.RemoverAsync(usuario);
                return (true, "Usuário removido com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao remover usuário: {ex.Message}");
            }
        }
    }
}
