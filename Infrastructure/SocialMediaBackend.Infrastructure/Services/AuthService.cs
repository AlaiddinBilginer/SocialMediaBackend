using Microsoft.AspNetCore.Identity;
using SocialMediaBackend.Application.DTOs.Auth;
using SocialMediaBackend.Application.Services;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return new AuthResponse() { Succeeded = false, Message = "Şifreler uyuşmamaktadır" };

            AppUser? userExist = await _userManager.FindByEmailAsync(request.Email);

            if (userExist != null)
                return new AuthResponse() { Succeeded = false, Message = "Girdiğiniz e-posta zaten kullanılmaktadır" };

            AppUser user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return new AuthResponse() { Succeeded = true, Message = "Kaydınız başarılı bir şekilde gerçekleşmiştir" };
            else
                return new AuthResponse() { Succeeded = false, Message = "Kayıt işlemi başarısız olmuştur" };
        }
    }
}
