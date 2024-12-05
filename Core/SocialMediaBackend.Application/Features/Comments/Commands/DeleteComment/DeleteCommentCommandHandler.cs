using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandRequest, DeleteCommentCommandResponse>
    {
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public DeleteCommentCommandHandler(
            ICommentWriteRepository commentWriteRepository,
            IHttpContextAccessor contextAccessor,
            UserManager<AppUser> userManager,
            ICommentReadRepository commentReadRepository)
        {
            _commentWriteRepository = commentWriteRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _commentReadRepository = commentReadRepository;
        }

        public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            Comment? comment = await _commentReadRepository.GetByIdAsync(request.Id);

            if(user == null || comment == null || user.Id != comment.AppUserId)
                return new DeleteCommentCommandResponse { Succeeded = false, Message = "Bu işlemi gerçekleştiremezsiniz" };

            await _commentWriteRepository.DeleteByIdAsync(request.Id);
            await _commentWriteRepository.SaveAsync();

            return new DeleteCommentCommandResponse { Succeeded = true, Message = "Yorumunuz başarılı bir şekilde silindi" };
        }
    }
}
