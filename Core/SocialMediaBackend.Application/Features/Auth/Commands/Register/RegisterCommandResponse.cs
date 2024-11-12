namespace SocialMediaBackend.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
