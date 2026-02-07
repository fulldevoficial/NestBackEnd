using Application.DTOs;

namespace Application.Ports.Input
{
    public interface IAuthUseCase
    {
        Task<(bool sucesso, string mensagem, string? token)> LoginAsync(LoginDto dto);
    }
}
