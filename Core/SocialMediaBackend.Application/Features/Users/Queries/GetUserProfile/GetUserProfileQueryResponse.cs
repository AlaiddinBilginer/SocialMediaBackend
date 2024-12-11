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
    }
}
