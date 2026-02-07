using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Usuario;

namespace Data.Configurations
{
    /// <summary>
    /// Configuração do Entity Framework para a entidade Usuario usando Fluent API
    /// Esta abordagem substitui DataAnnotations e oferece mais flexibilidade
    /// </summary>
    public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            // Nome da tabela
            builder.ToTable("usuarios");

            // Primary Key - ID (UUIDv7)
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever(); // ID será gerado pela aplicação usando UUIDv7

            // Unique Key - Código do Usuário
            builder.HasIndex(u => u.CodigoUsuario)
                .IsUnique()
                .HasDatabaseName("uk_usuarios_codigo");
            
            builder.Property(u => u.CodigoUsuario)
                .HasColumnName("codigo_usuario")
                .IsRequired();

            // Nome
            builder.Property(u => u.Nome)
                .HasColumnName("nome")
                .HasMaxLength(200)
                .IsRequired();

            // Email - também único
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("uk_usuarios_email");

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            // Password Hash
            builder.Property(u => u.Password)
                .HasColumnName("password_hash")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
