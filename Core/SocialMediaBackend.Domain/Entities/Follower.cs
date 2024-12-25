using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public sealed class Follower : Entity
    {
        public string FollowerUserId { get; set; }
        public AppUser FollowerUser { get; set; }

        public string FollowedUserId { get; set; }
        public AppUser FollowedUser { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
