namespace Application.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Selo { get; set; }
        public decimal? PrecoAtual { get; set; }
        public decimal? PrecoAntigo { get; set; }
        public string? Category { get; set; }
        public string[]? Tags { get; set; }
        public bool IsDestaque { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }

    public class CriarCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Selo { get; set; }
        public decimal? PrecoAtual { get; set; }
        public decimal? PrecoAntigo { get; set; }
        public string? Category { get; set; }
        public string[]? Tags { get; set; }
        public bool IsDestaque { get; set; }
    }

    public class AtualizarCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Selo { get; set; }
        public decimal? PrecoAtual { get; set; }
        public decimal? PrecoAntigo { get; set; }
        public string? Category { get; set; }
        public string[]? Tags { get; set; }
        public bool IsDestaque { get; set; }
    }

    public class PagedCourseResultDto
    {
        public IEnumerable<CourseDto> Items { get; set; } = new List<CourseDto>();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
