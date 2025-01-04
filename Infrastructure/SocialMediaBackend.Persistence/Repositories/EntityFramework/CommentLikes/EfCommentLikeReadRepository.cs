using SocialMediaBackend.Application.Repositories.CommentLikes;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;
using SocialMediaBackend.Persistence.Repositories.EntityFramework.Comments;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.CommentLikes;

public class EfCommentLikeReadRepository : EfReadRepository<CommentLike, SocialMediaDbContext>, ICommentLikeReadRepository
{
    public EfCommentLikeReadRepository(SocialMediaDbContext context) : base(context)
    {
    }
}
