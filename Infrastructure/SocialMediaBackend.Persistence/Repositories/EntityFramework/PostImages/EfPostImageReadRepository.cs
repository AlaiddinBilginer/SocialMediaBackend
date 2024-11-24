using SocialMediaBackend.Application.Repositories.PostImages;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.PostImages
{
    public class EfPostImageReadRepository : EfReadRepository<PostImage, SocialMediaDbContext>, IPostImageReadRepository
    {
        public EfPostImageReadRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
