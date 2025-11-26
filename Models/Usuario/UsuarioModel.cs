using System.ComponentModel.DataAnnotations;

namespace Models.Usuario
{
    public class UsuarioModel
    {
        [Key]
        public int CD_USUARIO { get; set; }
        public string? NOME { get; set; }
        public string? EMAIL { get; set; }
        public string? PASSWORD { get; set; }
    }
}
