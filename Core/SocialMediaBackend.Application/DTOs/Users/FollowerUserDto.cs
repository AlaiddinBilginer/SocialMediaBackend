namespace SocialMediaBackend.Application.DTOs.Users;

public class FollowerUserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string ProfilePhoto { get; set; }
    public bool IsFollowing { get; set; }
}
