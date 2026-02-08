namespace Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListarTodosAsync();
        Task<TEntity?> BuscarPorIdAsync(Guid id);
        Task AdicionarAsync(TEntity entity);
        Task AtualizarAsync(TEntity entity);
        Task RemoverAsync(TEntity entity);
    }
}
