using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Models.Login;
using Services.Helpers;
using Services.Interfaces.Login;
using Services.Interfaces.Usuario;

namespace Services.Implementation.Login
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _config;

        public AuthService(IUsuarioService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }

        public async Task<IEnumerable<object>> Autenticar(LoginRequest login)
        {
            var usuario = await _usuarioService.BuscarPorEmail(login.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Password, usuario.PASSWORD))
            {
                return new List<object>
                {
                    new { sucesso = false, mensagem = "Usuário ou senha inválidos." }
                };
            }

            var token = JwtTokenGenerator.GerarToken(usuario, _config);

            return new List<object>
            {
                new
                {
                    sucesso = true,
                    mensagem = "Login realizado com sucesso.",
                    token,
                    dados = new
                    {
                        usuario
                    }
                }
            };
        }

    }
}
