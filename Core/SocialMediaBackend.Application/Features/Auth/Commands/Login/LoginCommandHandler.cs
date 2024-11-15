using MediatR;
using SocialMediaBackend.Application.DTOs.Auth;
using SocialMediaBackend.Application.Services;

namespace SocialMediaBackend.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(new LoginRequest()
            {
                UserNameOrEmail = request.UserNameOrEmail,
                Password = request.Password,
            }, 15);

            if(response.Succeeded)
                return new LoginCommandSuccessResponse() { Message = response.Message, Token = response.Token };
            else
                return new LoginCommandErrorResponse() { Message = response.Message };



        }
    }
}
