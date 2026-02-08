using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBase(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> ListarTodosAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> BuscarPorIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task AdicionarAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task AtualizarAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task RemoverAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
