namespace SocialMediaBackend.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
