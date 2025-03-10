﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandRequest, DeleteCommentCommandResponse>
    {
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public DeleteCommentCommandHandler(
            ICommentWriteRepository commentWriteRepository,
            IHttpContextAccessor contextAccessor,
            UserManager<AppUser> userManager,
            ICommentReadRepository commentReadRepository,
            IPostReadRepository postReadRepository,
            IPostWriteRepository postWriteRepository)
        {
            _commentWriteRepository = commentWriteRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _commentReadRepository = commentReadRepository;
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
        }

        public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            PostComment? comment = await _commentReadRepository.GetByIdAsync(request.Id);

            if(user == null || comment == null || user.Id != comment.AppUserId)
                return new DeleteCommentCommandResponse { Succeeded = false, Message = "Bu işlemi gerçekleştiremezsiniz" };

            Post post = await _postReadRepository.GetByIdAsync(comment.PostId.ToString());

            await _commentWriteRepository.DeleteByIdAsync(request.Id);
            await _commentWriteRepository.SaveAsync();

            user.CommentsCount--;
            await _userManager.UpdateAsync(user);

            post.CommentCount--;
            await _commentWriteRepository.SaveAsync();

            return new DeleteCommentCommandResponse { Succeeded = true, Message = "Yorumunuz başarılı bir şekilde silindi" };
        }
    }
}
