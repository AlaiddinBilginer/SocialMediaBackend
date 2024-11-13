using SocialMediaBackend.Application.DTOs;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Services
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int accessTokenLifeTime, AppUser user);
    }
}
