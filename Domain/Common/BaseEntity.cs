namespace Domain.Common
{
    /// <summary>
    /// Classe base abstrata para todas as entidades do domínio
    /// </summary>
    public abstract class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.Empty;
        }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
