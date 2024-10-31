using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public class Friendship : Entity
    {
        public string RequesterId { get; set; } = string.Empty;
        public AppUser Requester { get; set; } = new AppUser();

        public string ReceiverId { get; set; } = string.Empty;
        public AppUser Receiver { get; set; } = new AppUser();

        public FriendshipStatus Status { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
