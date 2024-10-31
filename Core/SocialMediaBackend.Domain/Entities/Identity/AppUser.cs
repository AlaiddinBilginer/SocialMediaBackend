using Microsoft.AspNetCore.Identity;

namespace SocialMediaBackend.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; } 
        public string? Bio { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<MessageThreadParticipant> MessageThreads { get; set; } = new List<MessageThreadParticipant>();
    }
}
