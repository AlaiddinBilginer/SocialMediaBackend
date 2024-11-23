using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public class Comment : Entity
    {
        public string Content { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; }


    }
}
