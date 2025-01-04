using SocialMediaBackend.Application.Repositories.CommentLikes;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.CommentLikes;

public class EfCommentLikeWriteRepository : EfWriteRepository<CommentLike, SocialMediaDbContext>, ICommentLikeWriteRepository
{
    public EfCommentLikeWriteRepository(SocialMediaDbContext context) : base(context)
    {
    }
}
