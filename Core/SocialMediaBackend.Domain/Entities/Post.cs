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

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<PostImage> PostImages { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
