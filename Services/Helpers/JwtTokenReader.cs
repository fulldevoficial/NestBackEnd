using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public static class JwtTokenReader
    {
        public static int? ObterUsuarioId(HttpContext context)
        {
            var claim = context.User?.FindFirst("usuarioId")?.Value;
            return int.TryParse(claim, out var id) ? id : null;
        }

        public static bool UsuarioTokenEhValido(HttpContext context, int cdUsuario)
        {
            var idToken = ObterUsuarioId(context);
            return idToken.HasValue && idToken.Value == cdUsuario;
        }
    }
}
