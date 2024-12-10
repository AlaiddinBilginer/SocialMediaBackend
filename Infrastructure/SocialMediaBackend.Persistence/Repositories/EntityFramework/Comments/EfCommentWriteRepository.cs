using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Comments
{
    public class EfCommentWriteRepository : EfWriteRepository<PostComment, SocialMediaDbContext>, ICommentWriteRepository
    {
        public EfCommentWriteRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
