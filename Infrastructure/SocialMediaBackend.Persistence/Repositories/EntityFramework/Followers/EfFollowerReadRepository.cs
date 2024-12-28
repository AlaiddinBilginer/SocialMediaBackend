using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Followers
{
    public class EfFollowerReadRepository : EfReadRepository<Follower, SocialMediaDbContext>, IFollowersReadRepository
    {
        public EfFollowerReadRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
