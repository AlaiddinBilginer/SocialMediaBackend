using SocialMediaBackend.Application.Repositories;
using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Followers
{
    public class EfFollowerWriteRepository : EfWriteRepository<Follower, SocialMediaDbContext>, IFollowersWriteRepository
    {
        public EfFollowerWriteRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
