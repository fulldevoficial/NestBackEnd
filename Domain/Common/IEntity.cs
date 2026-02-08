namespace Domain.Common
{
    /// <summary>
    /// Interface base para todas as entidades do domínio
    /// </summary>
    /// <typeparam name="TId">Tipo do identificador da entidade</typeparam>
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
