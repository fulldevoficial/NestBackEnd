using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class CourseUseCase : ICourseUseCase
    {
        private readonly ICourseRepository _repository;
        private readonly IIdentityGenerator _identityGenerator;

        public CourseUseCase(
            ICourseRepository repository,
            IIdentityGenerator identityGenerator)
        {
            _repository = repository;
            _identityGenerator = identityGenerator;
        }

        public async Task<PagedCourseResultDto> ListarComFiltrosAsync(
            string? category,
            string? search,
            int page,
            int pageSize,
            string? sort)
        {
            var (items, totalCount) = await _repository.BuscarComFiltrosAsync(category, search, page, pageSize, sort);

            var courseDtos = items.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Thumbnail = c.Thumbnail,
                Selo = c.Selo,
                PrecoAtual = c.PrecoAtual,
                PrecoAntigo = c.PrecoAntigo,
                Category = c.Category,
                Tags = string.IsNullOrWhiteSpace(c.Tags) ? null : c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries),
                IsDestaque = c.IsDestaque,
                CriadoEm = c.CriadoEm,
                AtualizadoEm = c.AtualizadoEm
            });

            return new PagedCourseResultDto
            {
                Items = courseDtos,
                Total = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<CourseDto?> BuscarPorIdAsync(Guid id)
        {
            var course = await _repository.BuscarPorIdAsync(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Thumbnail = course.Thumbnail,
                Selo = course.Selo,
                PrecoAtual = course.PrecoAtual,
                PrecoAntigo = course.PrecoAntigo,
                Category = course.Category,
                Tags = string.IsNullOrWhiteSpace(course.Tags) ? null : course.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries),
                IsDestaque = course.IsDestaque,
                CriadoEm = course.CriadoEm,
                AtualizadoEm = course.AtualizadoEm
            };
        }

        public async Task<(bool sucesso, string mensagem, Guid? id)> CriarAsync(CriarCourseDto dto)
        {
            try
            {
                var id = _identityGenerator.Generate();
                var tags = dto.Tags != null && dto.Tags.Length > 0 ? string.Join(",", dto.Tags) : null;

                var course = new Course(
                    id,
                    dto.Title,
                    dto.Description,
                    dto.Thumbnail,
                    dto.Selo,
                    dto.PrecoAtual,
                    dto.PrecoAntigo,
                    dto.Category,
                    tags,
                    dto.IsDestaque);

                await _repository.AdicionarAsync(course);
                return (true, "Curso criado com sucesso.", id);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar curso: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarAsync(Guid id, AtualizarCourseDto dto)
        {
            var course = await _repository.BuscarPorIdAsync(id);
            if (course == null)
                return (false, "Curso não encontrado.");

            try
            {
                var tags = dto.Tags != null && dto.Tags.Length > 0 ? string.Join(",", dto.Tags) : null;

                course.AtualizarDados(
                    dto.Title,
                    dto.Description,
                    dto.Thumbnail,
                    dto.Selo,
                    dto.PrecoAtual,
                    dto.PrecoAntigo,
                    dto.Category,
                    tags,
                    dto.IsDestaque);

                await _repository.AtualizarAsync(course);
                return (true, "Curso atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar curso: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> RemoverAsync(Guid id)
        {
            var course = await _repository.BuscarPorIdAsync(id);
            if (course == null)
                return (false, "Curso não encontrado.");

            try
            {
                await _repository.RemoverAsync(course);
                return (true, "Curso removido com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao remover curso: {ex.Message}");
            }
        }
    }
}
