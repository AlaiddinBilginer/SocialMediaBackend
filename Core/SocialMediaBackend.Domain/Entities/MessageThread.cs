using SocialMediaBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public class MessageThread : Entity
    {
        public ICollection<MessageThreadParticipant> Participants { get; set; } = new List<MessageThreadParticipant>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
