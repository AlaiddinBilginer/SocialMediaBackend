using Microsoft.AspNetCore.Identity;

namespace SocialMediaBackend.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string FullName { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; } 
        public string? Bio { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<MessageThreadParticipant> MessageThreads { get; set; }
    }
}
