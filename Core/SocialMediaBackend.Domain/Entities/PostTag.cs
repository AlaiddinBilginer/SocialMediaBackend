using SocialMediaBackend.Domain.Entities.Base;

namespace SocialMediaBackend.Domain.Entities
{
    public sealed class PostTag : Entity
    {
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
