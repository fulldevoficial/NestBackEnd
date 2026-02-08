using Application.DTOs;
using Application.Interfaces;
using Domain.Repositories;

namespace Application.UseCases
{
    public class AuthUseCase : IAuthUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthUseCase(IUsuarioRepository repository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<(bool sucesso, string mensagem, string? token)> LoginAsync(LoginDto dto)
        {
            var usuario = await _repository.BuscarPorEmailAsync(dto.Email);

            if (usuario == null)
                return (false, "Usuário não encontrado.", null);

            if (!_passwordHasher.VerifyPassword(dto.Password, usuario.PasswordHash))
                return (false, "Senha incorreta.", null);

            var token = _tokenGenerator.GenerateToken(usuario.Email, usuario.Id);
            return (true, "Login realizado com sucesso.", token);
        }
    }
}
