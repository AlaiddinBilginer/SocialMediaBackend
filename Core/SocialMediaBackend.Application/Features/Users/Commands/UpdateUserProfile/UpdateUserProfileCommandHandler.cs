using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Exceptions;

namespace SocialMediaBackend.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommandRequest, UpdateUserProfileCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UpdateUserProfileCommandHandler(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<UpdateUserProfileCommandResponse> Handle(UpdateUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            if (userName == null)
                throw new UserNotFoundException();

            AppUser? isUserExist = await _userManager.FindByNameAsync(request.UserName);
            if (isUserExist != null)
                return new UpdateUserProfileCommandResponse { Succeeded = false, Message = "Bu kullanıcı ismine sahip bir kullanıcı zaten mevcut" };

            AppUser? editUser = await _userManager.FindByNameAsync(userName);

            if (editUser == null)
                throw new UserNotFoundException();

            editUser.UserName = request.UserName;
            editUser.FullName = request.FullName;
            editUser.Bio = request.Bio;
            editUser.NormalizedUserName = request.UserName.ToUpper();

            await _userManager.UpdateAsync(editUser);
            return new UpdateUserProfileCommandResponse { Succeeded = true, Message = "Kullanıcı bilgileriniz başarılı bir şekilde güncellenmiştir." };
        }
    }
}
