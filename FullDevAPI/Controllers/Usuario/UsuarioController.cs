using Application.DTOs;
using Application.Ports.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullDevAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioUseCase _usuarioUseCase;

        public UsuarioController(IUsuarioUseCase usuarioUseCase)
        {
            _usuarioUseCase = usuarioUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioUseCase.ListarTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioUseCase.BuscarPorIdAsync(id);
            return usuario is null
                ? NotFound(new { sucesso = false, mensagem = "Usuário não encontrado." })
                : Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CriarUsuarioDto dto)
        {
            var (sucesso, mensagem) = await _usuarioUseCase.CriarAsync(dto);

            if (sucesso)
                return CreatedAtAction(nameof(GetUsuario), new { id = 0 }, new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] AtualizarUsuarioDto dto)
        {
            var (sucesso, mensagem) = await _usuarioUseCase.AtualizarAsync(id, dto);

            if (sucesso) return Ok(new { sucesso, mensagem });
            if (mensagem == "Usuário não encontrado.") return NotFound(new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var (sucesso, mensagem) = await _usuarioUseCase.RemoverAsync(id);

            if (sucesso) return Ok(new { sucesso, mensagem });
            if (mensagem == "Usuário não encontrado.") return NotFound(new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }
    }
}
