using Application.DTOs;
using Application.Ports.Input;
using Application.Ports.Output;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IIdentityGenerator _identityGenerator;

        public UsuarioUseCase(
            IUsuarioRepository repository, 
            IPasswordHasher passwordHasher,
            IIdentityGenerator identityGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _identityGenerator = identityGenerator;
        }

        public async Task<IEnumerable<UsuarioDto>> ListarTodosAsync()
        {
            var usuarios = await _repository.ListarTodosAsync();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                CodigoUsuario = u.CodigoUsuario,
                Nome = u.Nome,
                Email = u.Email
            });
        }

        public async Task<UsuarioDto?> BuscarPorIdAsync(Guid id)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                CodigoUsuario = usuario.CodigoUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<UsuarioDto?> BuscarPorCodigoAsync(int codigo)
        {
            var usuario = await _repository.BuscarPorCodigoAsync(codigo);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                CodigoUsuario = usuario.CodigoUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<(bool sucesso, string mensagem, Guid? id)> CriarAsync(CriarUsuarioDto dto)
        {
            try
            {
                // Gera um novo UUIDv7
                var id = _identityGenerator.Generate();

                var passwordHash = _passwordHasher.HashPassword(dto.Password);
                var usuario = new Usuario(id, dto.CodigoUsuario, dto.Nome, dto.Email, passwordHash);

                await _repository.AdicionarAsync(usuario);
                return (true, "Usuário criado com sucesso.", id);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar usuário: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarAsync(Guid id, AtualizarUsuarioDto dto)
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

        public async Task<(bool sucesso, string mensagem)> RemoverAsync(Guid id)
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
