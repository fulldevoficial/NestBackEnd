using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Login;
using Services.Interfaces.Login;

namespace FullDevAPI.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IEnumerable<object>> Login([FromBody] LoginRequest login)
        {
            return await _auth.Autenticar(login);
        }
    }
}
