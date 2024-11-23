using SocialMediaBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public class MessageThread : Entity
    {
        public ICollection<MessageThreadParticipant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
