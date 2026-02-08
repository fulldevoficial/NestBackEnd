using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CourseDbMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(2000);

            builder.Property(c => c.Thumbnail)
                .HasMaxLength(1000);

            builder.Property(c => c.Selo)
                .HasMaxLength(100);

            builder.Property(c => c.PrecoAtual)
                .HasPrecision(18, 2);

            builder.Property(c => c.PrecoAntigo)
                .HasPrecision(18, 2);

            builder.Property(c => c.Category)
                .HasMaxLength(100);

            builder.HasIndex(c => c.Category)
                .HasDatabaseName("ix_courses_category");

            builder.Property(c => c.Tags)
                .HasMaxLength(500);

            builder.Property(c => c.IsDestaque);

            builder.Property(c => c.CriadoEm)
                .IsRequired();

            builder.Property(c => c.AtualizadoEm);

            builder.HasIndex(c => c.Title)
                .HasDatabaseName("ix_courses_title");
        }
    }
}
