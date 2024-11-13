using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaBackend.Application.DTOs;
using SocialMediaBackend.Application.Services;
using SocialMediaBackend.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaBackend.Infrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int accessTokenLifeTime, AppUser user)
        {
            Token token = new Token();

            var securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            var signingCredentials = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(accessTokenLifeTime);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
            };

            if (!string.IsNullOrEmpty(user.FullName))
                claims.Add(new Claim("FullName", user.FullName));

            if (!string.IsNullOrEmpty(user.ProfilePhoto))
                claims.Add(new Claim("ProfilePhoto", user.ProfilePhoto));

            if (!string.IsNullOrEmpty(user.CoverPhoto))
                claims.Add(new Claim("CoverPhoto", user.CoverPhoto));

            if (!string.IsNullOrEmpty(user.Bio))
                claims.Add(new Claim("Bio", user.Bio));

            JwtSecurityToken securityToken = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
            );

            var handler = new JwtSecurityTokenHandler();
            token.AccessToken = handler.WriteToken(securityToken);

            return token;
        }
    }
}
