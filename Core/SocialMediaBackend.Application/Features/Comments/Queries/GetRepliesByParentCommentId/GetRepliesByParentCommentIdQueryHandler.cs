using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.Application.DTOs.Comments;
using SocialMediaBackend.Application.Repositories.CommentLikes;
using SocialMediaBackend.Application.Repositories.Comments;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetRepliesByParentCommentId
{
    public class GetRepliesByParentCommentIdQueryHandler : IRequestHandler<GetRepliesByParentCommentIdQueryRequest, GetRepliesByParentCommentIdQueryResponse>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICommentLikeReadRepository _commentLikeReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetRepliesByParentCommentIdQueryHandler(
            ICommentReadRepository commentReadRepository, 
            ICommentLikeReadRepository commentLikeReadRepository,
            ICurrentUserService currentUserService)
        {
            _commentReadRepository = commentReadRepository;
            _commentLikeReadRepository = commentLikeReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetRepliesByParentCommentIdQueryResponse> Handle(GetRepliesByParentCommentIdQueryRequest request, CancellationToken cancellationToken)
        {
            var replies = _commentReadRepository.GetWhere(x => x.ParentCommentId == Guid.Parse(request.ParentCommentId))
                .Include(x => x.Likes)
                .OrderBy(x => x.CreatedDate)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Select(r => new ReplyCommentDto
                {
                    Id = r.Id.ToString(),
                    UserId = r.AppUserId,
                    Content = r.Content,
                    UserName = r.AppUser.UserName,
                    UserProfilePhoto = r.AppUser.ProfilePhoto,
                    LikeCount = r.LikeCount,
                    IsLiked = r.Likes.Where(x => x.UserId == _currentUserService.UserId).Any(),
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                });

            return new GetRepliesByParentCommentIdQueryResponse { Replies = replies };
        }
    }
}
