using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryResponse
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Bio { get; set; }
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsFollower { get; set; }
        public DateTime AccountCreatedDate { get; set; }
    }
}
