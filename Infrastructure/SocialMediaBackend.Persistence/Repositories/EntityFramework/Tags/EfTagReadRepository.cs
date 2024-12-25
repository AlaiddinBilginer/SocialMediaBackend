using SocialMediaBackend.Application.Repositories.Tags;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Tags
{
    public class EfTagReadRepository : EfReadRepository<PostTag, SocialMediaDbContext>, ITagReadRepository
    {
        public EfTagReadRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
