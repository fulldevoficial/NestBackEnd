using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICourseUseCase
    {
        Task<PagedCourseResultDto> ListarComFiltrosAsync(
            string? category,
            string? search,
            int page,
            int pageSize,
            string? sort);

        Task<CourseDto?> BuscarPorIdAsync(Guid id);

        Task<(bool sucesso, string mensagem, Guid? id)> CriarAsync(CriarCourseDto dto);

        Task<(bool sucesso, string mensagem)> AtualizarAsync(Guid id, AtualizarCourseDto dto);

        Task<(bool sucesso, string mensagem)> RemoverAsync(Guid id);
    }
}
