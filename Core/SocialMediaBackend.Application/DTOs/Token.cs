namespace SocialMediaBackend.Application.DTOs
{
    public class Token
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
    