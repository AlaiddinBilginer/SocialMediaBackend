using SocialMediaBackend.Application.Repositories.PostLikes;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.PostLikes;

public class EfPostLikeReadRepository : EfReadRepository<PostLike, SocialMediaDbContext>, IPostLikeReadRepository
{
    public EfPostLikeReadRepository(SocialMediaDbContext context) : base(context)
    {
    }
}
