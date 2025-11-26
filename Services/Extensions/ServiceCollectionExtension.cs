using Microsoft.Extensions.DependencyInjection;
using Services.Implementation.Login;
using Services.Implementation.Usuario;
using Services.Interfaces.Login;
using Services.Interfaces.Usuario;

namespace Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
