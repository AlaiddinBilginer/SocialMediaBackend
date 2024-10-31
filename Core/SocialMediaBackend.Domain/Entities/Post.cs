using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public class Post : Entity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = new Category();

        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = new AppUser();

        public ICollection<PostImage> PostImages { get; set; } = new List<PostImage>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
