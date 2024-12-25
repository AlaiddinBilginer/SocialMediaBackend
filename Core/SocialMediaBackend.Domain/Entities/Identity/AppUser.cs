using Microsoft.AspNetCore.Identity;

namespace SocialMediaBackend.Domain.Entities.Identity
{
    public sealed class AppUser : IdentityUser<string>
    {
        public string FullName { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; } 
        public string? Bio { get; set; }
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public DateTime AccountCreatedDate { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
        public ICollection<Follower> Followers { get; set; }
        public ICollection<Follower> Following { get; set; }
    }
}
