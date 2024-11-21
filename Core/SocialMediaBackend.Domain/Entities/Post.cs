using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public class Post : Entity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; }

        public ICollection<PostImage> PostImages { get; set; } = new List<PostImage>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
