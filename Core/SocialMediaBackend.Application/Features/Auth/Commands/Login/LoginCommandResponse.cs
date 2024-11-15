using SocialMediaBackend.Application.DTOs;

namespace SocialMediaBackend.Application.Features.Auth.Commands.Login
{
    public class LoginCommandResponse
    {
        public string Message { get; set; } = string.Empty;
    }

    public class LoginCommandSuccessResponse : LoginCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public Token Token { get; set; }
    }

    public class LoginCommandErrorResponse : LoginCommandResponse
    {
        public bool Succeeded { get; set; } = false;
    }
}
