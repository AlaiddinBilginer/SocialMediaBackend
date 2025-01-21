using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public sealed class Post : Entity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public Guid CategoryId { get; set; }
        public PostCategory Category { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<PostImage> PostImages { get; set; }
        public ICollection<PostTag> Tags { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<PostLike> Likes { get; set; }
    }
}
