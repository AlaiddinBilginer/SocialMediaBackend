using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommandRequest, UpdateCommentCommandResponse>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UpdateCommentCommandHandler(
            ICommentReadRepository commentReadRepository,
            IHttpContextAccessor contextAccessor,
            UserManager<AppUser> userManager,
            ICommentWriteRepository commentWriteRepository)
        {
            _commentReadRepository = commentReadRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _commentWriteRepository = commentWriteRepository;
        }

        public async Task<UpdateCommentCommandResponse> Handle(UpdateCommentCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            PostComment? comment = await _commentReadRepository.GetByIdAsync(request.CommentId);

            if(user == null || user.Id != comment.AppUserId || comment == null)
                return new UpdateCommentCommandResponse { Succeeded = false, Message = "Bu işlemi gerçekleştiremezsiniz" };

            comment.Content = request.Content;
            await _commentWriteRepository.SaveAsync();

            return new UpdateCommentCommandResponse { Succeeded = true, Message = "Yorumunuz başarılı bir şekilde güncellendi" };
        }
    }
}
