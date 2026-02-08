using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullDevAPI.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUseCase _authUseCase;

        public AuthController(IAuthUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var (sucesso, mensagem, token) = await _authUseCase.LoginAsync(dto);

            if (sucesso)
                return Ok(new { sucesso, mensagem, token });

            return Unauthorized(new { sucesso, mensagem });
        }
    }
}
