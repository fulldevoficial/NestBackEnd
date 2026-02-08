namespace Domain.Common
{
    /// <summary>
    /// Interface para geração de identificadores únicos
    /// </summary>
    public interface IIdentityGenerator
    {
        /// <summary>
        /// Gera um novo identificador único usando UUIDv7
        /// UUIDv7 contém timestamp permitindo ordenação temporal e recuperação de datas
        /// </summary>
        Guid Generate();

        /// <summary>
        /// Extrai o timestamp de um UUIDv7
        /// </summary>
        DateTimeOffset ExtractTimestamp(Guid uuidv7);
    }
}
