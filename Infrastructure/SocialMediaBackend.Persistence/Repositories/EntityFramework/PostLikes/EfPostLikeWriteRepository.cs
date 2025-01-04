using SocialMediaBackend.Application.Repositories.PostLikes;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.PostLikes;

public class EfPostLikeWriteRepository : EfWriteRepository<PostLike, SocialMediaDbContext>, IPostLikeWriteRepository
{
    public EfPostLikeWriteRepository(SocialMediaDbContext context) : base(context)
    {
    }
}
