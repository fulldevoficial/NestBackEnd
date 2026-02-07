using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Usuario;

namespace Data.Configurations
{
    public class UsuarioDbMapping : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedNever();

            builder.HasIndex(u => u.CodigoUsuario)
                .IsUnique()
                .HasDatabaseName("uk_usuarios_codigo");

            builder.Property(u => u.Nome)
                .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("uk_usuarios_email");

            builder.Property(u => u.Email)
                .HasMaxLength(255);
        }
    }
}
