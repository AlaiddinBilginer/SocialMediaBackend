using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Persistence.Contexts;
using SocialMediaBackend.Persistence.Repositories.EntityFramework.Categories;

namespace SocialMediaBackend.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMediaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
            });

            services.AddScoped<ICategoryWriteRepository, EfCategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, EfCategoryReadRepository>();
        }
    }
}
