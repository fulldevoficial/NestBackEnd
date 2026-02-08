using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("CD_USUARIO");

            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                .HasColumnName("PASSWORD")
                .IsRequired();
        }
    }
}
