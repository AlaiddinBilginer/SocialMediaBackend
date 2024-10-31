using SocialMediaBackend.Domain.Entities.Base;

namespace SocialMediaBackend.Domain.Entities
{
    public class Category : Entity
    {
        public string Title { get; set; } = string.Empty;
        public string? Photo { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
