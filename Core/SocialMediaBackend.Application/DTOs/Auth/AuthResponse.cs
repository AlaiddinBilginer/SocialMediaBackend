namespace SocialMediaBackend.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
