using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Comments
{
    public class EfCommentReadRepository : EfReadRepository<PostComment, SocialMediaDbContext>, ICommentReadRepository
    {
        public EfCommentReadRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
