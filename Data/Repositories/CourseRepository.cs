using Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Course> Items, int TotalCount)> BuscarComFiltrosAsync(
            string? category,
            string? search,
            int page,
            int pageSize,
            string? sort)
        {
            var query = DbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(c => c.Category != null && c.Category.ToLower() == category.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchLower = search.ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(searchLower) ||
                                        (c.Description != null && c.Description.ToLower().Contains(searchLower)) ||
                                        (c.Tags != null && c.Tags.ToLower().Contains(searchLower)));
            }

            query = sort?.ToLower() switch
            {
                "title" => query.OrderBy(c => c.Title),
                "title_desc" => query.OrderByDescending(c => c.Title),
                "price" => query.OrderBy(c => c.PrecoAtual),
                "price_desc" => query.OrderByDescending(c => c.PrecoAtual),
                "date" => query.OrderBy(c => c.CriadoEm),
                "date_desc" => query.OrderByDescending(c => c.CriadoEm),
                _ => query.OrderByDescending(c => c.IsDestaque).ThenByDescending(c => c.CriadoEm)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
