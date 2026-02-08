using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullDevAPI.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseUseCase _courseUseCase;

        public CourseController(ICourseUseCase courseUseCase)
        {
            _courseUseCase = courseUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(
            [FromQuery] string? category = null,
            [FromQuery] string? search = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sort = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var result = await _courseUseCase.ListarComFiltrosAsync(category, search, page, pageSize, sort);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            var course = await _courseUseCase.BuscarPorIdAsync(id);
            return course is null
                ? NotFound(new { sucesso = false, mensagem = "Curso não encontrado." })
                : Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CriarCourseDto dto)
        {
            var (sucesso, mensagem, id) = await _courseUseCase.CriarAsync(dto);

            if (sucesso)
                return CreatedAtAction(nameof(GetCourse), new { id }, new { sucesso, mensagem, id });

            return StatusCode(500, new { sucesso, mensagem });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] AtualizarCourseDto dto)
        {
            var (sucesso, mensagem) = await _courseUseCase.AtualizarAsync(id, dto);

            if (sucesso) return Ok(new { sucesso, mensagem });
            if (mensagem == "Curso não encontrado.") return NotFound(new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var (sucesso, mensagem) = await _courseUseCase.RemoverAsync(id);

            if (sucesso) return Ok(new { sucesso, mensagem });
            if (mensagem == "Curso não encontrado.") return NotFound(new { sucesso, mensagem });

            return StatusCode(500, new { sucesso, mensagem });
        }
    }
}
