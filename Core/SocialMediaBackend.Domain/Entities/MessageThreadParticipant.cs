﻿using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public class MessageThreadParticipant : Entity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Guid MessageThreadId { get; set; }
        public MessageThread MessageThread { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
