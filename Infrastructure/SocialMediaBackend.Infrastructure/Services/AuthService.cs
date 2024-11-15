using Microsoft.AspNetCore.Identity;
using SocialMediaBackend.Application.DTOs;
using SocialMediaBackend.Application.DTOs.Auth;
using SocialMediaBackend.Application.Services;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
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

        public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest, int accessTokenLifeTime)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserNameOrEmail)
                ?? await _userManager.FindByEmailAsync(loginRequest.UserNameOrEmail);

            if (user == null)
                return new AuthResponse() { Succeeded = false, Message = "Kullanıcı adı veya E-posta hatalı" };

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

            if(!result.Succeeded)
                return new AuthResponse() { Succeeded = false, Message = "Hatalı şifre girdiniz"};

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

            return new AuthResponse() { Succeeded = true, Message = "Giriş başarı ile gerçekleşti", Token = token };
        }
    }
}
