using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Application.Repositories.PostImages;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Persistence.Contexts;
using SocialMediaBackend.Persistence.Repositories.EntityFramework.Categories;
using SocialMediaBackend.Persistence.Repositories.EntityFramework.Comments;
using SocialMediaBackend.Persistence.Repositories.EntityFramework.PostImages;
using SocialMediaBackend.Persistence.Repositories.EntityFramework.Posts;

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

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<SocialMediaDbContext>();

            services.AddScoped<ICategoryWriteRepository, EfCategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, EfCategoryReadRepository>();

            services.AddScoped<IPostWriteRepository, EfPostWriteRepository>();
            services.AddScoped<IPostReadRepository, EfPostReadRepository>();

            services.AddScoped<IPostImageWriteRepository, EfPostImageWriteRepository>();
            services.AddScoped<IPostImageReadRepository, EfPostImageReadRepository>();

            services.AddScoped<ICommentReadRepository, EfCommentReadRepository>();
            services.AddScoped<ICommentWriteRepository, EfCommentWriteRepository>();
        }
    }
}
