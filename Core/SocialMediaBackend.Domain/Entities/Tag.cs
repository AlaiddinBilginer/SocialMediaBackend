using SocialMediaBackend.Domain.Entities.Base;

namespace SocialMediaBackend.Domain.Entities
{
    public class Tag : Entity
    {
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
