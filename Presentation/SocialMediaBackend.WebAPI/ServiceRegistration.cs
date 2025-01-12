using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.WebAPI.Services;

namespace SocialMediaBackend.WebAPI;

public static class ServiceRegistration
{
    public static void AddWebApiServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
