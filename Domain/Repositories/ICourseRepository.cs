using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<(IEnumerable<Course> Items, int TotalCount)> BuscarComFiltrosAsync(
            string? category,
            string? search,
            int page,
            int pageSize,
            string? sort);
    }
}
