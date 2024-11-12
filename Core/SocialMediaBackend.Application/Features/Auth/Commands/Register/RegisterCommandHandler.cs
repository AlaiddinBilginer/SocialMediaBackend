using MediatR;
using SocialMediaBackend.Application.DTOs.Auth;
using SocialMediaBackend.Application.Services;

namespace SocialMediaBackend.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(new RegisterRequest()
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
            });

            return new RegisterCommandResponse() { Succeeded = response.Succeeded, Message = response.Message };
        }
    }
}
