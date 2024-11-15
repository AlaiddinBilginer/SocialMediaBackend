using SocialMediaBackend.Application.DTOs.Auth;

namespace SocialMediaBackend.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest loginRequest, int accessTokenLifeTime);
    }
}
