using Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Login
{
    public interface IAuthService
    {
        Task<IEnumerable<object>> Autenticar(LoginRequest login);
    }
}
