using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommandRequest, CreateCommentCommandResponse>
    {
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public CreateCommentCommandHandler(
            ICommentWriteRepository commentWriteRepository,
            IHttpContextAccessor contextAccessor,
            UserManager<AppUser> userManager,
            IPostReadRepository postReadRepository)
        {
            _commentWriteRepository = commentWriteRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _postReadRepository = postReadRepository;
        }

        public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            Post? post = await _postReadRepository.GetByIdAsync(request.PostId);

            if(user == null || post == null)
                return new CreateCommentCommandResponse { Succeeded = false, Message = "Bu işlemi gerçekleştiremezsiniz" };

            PostComment comment = new PostComment
            {
                PostId = Guid.Parse(request.PostId),
                AppUserId = user.Id,
                Content = request.Content
            };

            if(request.ParentCommentId != null)
                comment.ParentCommentId = Guid.Parse(request.ParentCommentId);

            await _commentWriteRepository.AddAsync(comment);

            await _commentWriteRepository.SaveAsync();

            return new CreateCommentCommandResponse { Succeeded = true, Message = "Yorumunuz başarılı bir şekilde eklendi" };
        }
    }
}
