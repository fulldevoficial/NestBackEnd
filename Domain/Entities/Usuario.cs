namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        private Usuario() { }

        public Usuario(string nome, string email, string passwordHash)
        {
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
