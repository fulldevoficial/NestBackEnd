namespace Models.Usuario
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public int CodigoUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
