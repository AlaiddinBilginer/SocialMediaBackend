using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Posts
{
    public class EfPostWriteRepository : EfWriteRepository<Post, SocialMediaDbContext>, IPostWriteRepository
    {
        public EfPostWriteRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
