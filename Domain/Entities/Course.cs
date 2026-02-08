using Domain.Common;

namespace Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public string? Thumbnail { get; private set; }
        public string? Selo { get; private set; }
        public decimal? PrecoAtual { get; private set; }
        public decimal? PrecoAntigo { get; private set; }
        public string? Category { get; private set; }
        public string? Tags { get; private set; }
        public bool IsDestaque { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime? AtualizadoEm { get; private set; }

        private Course() : base() { }

        public Course(
            Guid id,
            string title,
            string? description = null,
            string? thumbnail = null,
            string? selo = null,
            decimal? precoAtual = null,
            decimal? precoAntigo = null,
            string? category = null,
            string? tags = null,
            bool isDestaque = false) : base(id)
        {
            Title = title;
            Description = description;
            Thumbnail = thumbnail;
            Selo = selo;
            PrecoAtual = precoAtual;
            PrecoAntigo = precoAntigo;
            Category = category;
            Tags = tags;
            IsDestaque = isDestaque;
            CriadoEm = DateTime.UtcNow;
        }

        public void AtualizarDados(
            string title,
            string? description,
            string? thumbnail,
            string? selo,
            decimal? precoAtual,
            decimal? precoAntigo,
            string? category,
            string? tags,
            bool isDestaque)
        {
            Title = title;
            Description = description;
            Thumbnail = thumbnail;
            Selo = selo;
            PrecoAtual = precoAtual;
            PrecoAntigo = precoAntigo;
            Category = category;
            Tags = tags;
            IsDestaque = isDestaque;
            AtualizadoEm = DateTime.UtcNow;
        }
    }
}
