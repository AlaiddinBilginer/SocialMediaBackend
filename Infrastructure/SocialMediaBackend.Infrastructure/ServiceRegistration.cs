using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Services;
using SocialMediaBackend.Infrastructure.Services;

namespace SocialMediaBackend.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
