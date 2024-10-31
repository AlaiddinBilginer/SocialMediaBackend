using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public class Comment : Entity
    {
        public string Content { get; set; } = string.Empty;

        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = new AppUser();

        public Guid PostId { get; set; }
        public Post Post { get; set; } = new Post();

        public Guid? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; } = new Comment();

        public ICollection<Comment> Replies { get; set; } = new List<Comment>();


    }
}
