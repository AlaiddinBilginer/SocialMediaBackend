using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public sealed class CommentLike : Entity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Guid CommentId { get; set; }
        public PostComment Comment { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
