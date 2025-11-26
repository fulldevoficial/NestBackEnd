using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Usuario;
using Services.Interfaces.Usuario;
using System.Linq;
using System.Threading.Tasks;

namespace FullDevAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuario;

        public UsuarioController(IUsuarioService usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuario.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuario.BuscarPorId(id);
            return usuario is null
                ? NotFound(new { sucesso = false, mensagem = "Usuário não encontrado." })
                : Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioModel usuario)
        {
            var (sucesso, mensagem) = await _usuario.Criar(usuario);

            if (sucesso)
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.CD_USUARIO }, new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            if (id != usuario.CD_USUARIO)
                return BadRequest(new { sucesso = false, mensagem = "ID no corpo não corresponde ao parâmetro da rota." });

            var (sucesso, mensagem) = await _usuario.Atualizar(id, usuario);

            if (sucesso) return Ok(new { sucesso, mensagem });
            if (mensagem == "Usuário não encontrado.") return NotFound(new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var (sucesso, mensagem) = await _usuario.Remover(id);

            if (sucesso) return Ok(new { sucesso, mensagem });
            if (mensagem == "Usuário não encontrado.") return NotFound(new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }

    }
}
