using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Services;
using SocialMediaBackend.Application.Services.Storage;
using SocialMediaBackend.Infrastructure.Services;
using SocialMediaBackend.Infrastructure.Services.Storage;

namespace SocialMediaBackend.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
