using SocialMediaBackend.Application.Repositories.PostImages;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.PostImages
{
    public class EfPostImageWriteRepository : EfWriteRepository<PostImage, SocialMediaDbContext>, IPostImageWriteRepository
    {
        public EfPostImageWriteRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
