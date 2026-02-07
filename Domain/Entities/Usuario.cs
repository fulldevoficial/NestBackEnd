using Domain.Common;

namespace Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public int CodigoUsuario { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        private Usuario() : base() { }

        public Usuario(Guid id, int codigoUsuario, string nome, string email, string passwordHash) : base(id)
        {
            CodigoUsuario = codigoUsuario;
            Nome = nome;
            Email = email;
            PasswordHash = passwordHash;
        }

        public void AtualizarDados(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void AtualizarSenha(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
