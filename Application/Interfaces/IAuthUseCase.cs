using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthUseCase
    {
        Task<(bool sucesso, string mensagem, string? token)> LoginAsync(LoginDto dto);
    }
}
