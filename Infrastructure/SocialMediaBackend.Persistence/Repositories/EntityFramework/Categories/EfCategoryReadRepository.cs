using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Categories
{
    public class EfCategoryReadRepository : EfReadRepository<Category, SocialMediaDbContext>, ICategoryReadRepository
    {
        public EfCategoryReadRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
