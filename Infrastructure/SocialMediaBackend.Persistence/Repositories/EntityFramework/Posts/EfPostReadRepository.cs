using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Posts
{
    public class EfPostReadRepository : EfReadRepository<Post, SocialMediaDbContext>, IPostReadRepository
    {
        public EfPostReadRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
