using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.Application.DTOs.Comments;
using SocialMediaBackend.Application.Repositories.Comments;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQueryRequest, GetCommentsByPostIdQueryResponse>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCommentsByPostIdQueryHandler(ICommentReadRepository commentReadRepository, ICurrentUserService currentUserService)
        {
            _commentReadRepository = commentReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetCommentsByPostIdQueryResponse> Handle(GetCommentsByPostIdQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCommentCount = _commentReadRepository.GetAll(false)
                .Where(x => x.PostId == Guid.Parse(request.PostId))
                .Where(x => x.ParentCommentId == null)
                .Count();

            var comments = _commentReadRepository.GetAll(false).Where(x => x.PostId == Guid.Parse(request.PostId))
                .Where(x => x.ParentCommentId == null)
                .OrderByDescending(x => x.AppUserId == request.UserId)
                .ThenByDescending(x => x.CreatedDate)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Include(x => x.Likes)
                .Select(c => new CommentDto
                {
                    Id = c.Id.ToString(),
                    Content = c.Content,
                    UserId = c.AppUserId,
                    UserName = c.AppUser.UserName,
                    UserProfilePhoto = c.AppUser.ProfilePhoto,
                    TotalRepliesCount = c.Replies.Count(),
                    IsLiked = c.Likes.Where(x => x.UserId == _currentUserService.UserId).Any(),
                    LikeCount = c.LikeCount,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate
                });

            return new GetCommentsByPostIdQueryResponse { TotalCommentCount = totalCommentCount, Comments = comments };
        }
    }
}
