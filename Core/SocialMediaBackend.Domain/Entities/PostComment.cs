using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public sealed class PostComment : Entity
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid? ParentCommentId { get; set; }
        public PostComment ParentComment { get; set; }

        public ICollection<PostComment> Replies { get; set; }
        public ICollection<CommentLike> Likes { get; set; }


    }
}
