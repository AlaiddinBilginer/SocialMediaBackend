using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Domain.Entities
{
    public class Message : Entity
    {
        public string Content { get; set; } = string.Empty;

        public Guid SenderId { get; set; }
        public AppUser AppUser { get; set; } = new AppUser();

        public Guid MessageThreadId { get; set; }
        public MessageThread MessageThread { get; set; } = new MessageThread();
    }
}
