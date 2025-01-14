using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.Application.DTOs.Comments;
using SocialMediaBackend.Application.Repositories.Comments;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetCommentsByUser
{
    public class GetCommentsByUserQueryHandler : IRequestHandler<GetCommentsByUserQueryRequest, GetCommentsByUserQueryResponse>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCommentsByUserQueryHandler(ICommentReadRepository commentReadRepository, ICurrentUserService currentUserService)
        {
            _commentReadRepository = commentReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetCommentsByUserQueryResponse> Handle(GetCommentsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCommentsCount = _commentReadRepository.GetWhere(x => x.AppUser.UserName == request.UserName).Count();
            var comments = _commentReadRepository.GetWhere(x => x.AppUser.UserName == request.UserName, false)
                .Include(x => x.Likes)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Select(x => new GetCommentsByUserDto
                {
                    CommentId = x.Id.ToString(),
                    PostId = x.PostId.ToString(),
                    UserId = x.AppUserId,
                    Content = x.Content,
                    UserName = x.AppUser.UserName,
                    UserProfilePhoto = x.AppUser.ProfilePhoto,
                    IsLiked = x.Likes.Where(x => x.UserId == _currentUserService.UserId).Any(),
                    LikeCount = x.LikeCount,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate
                });

            return new GetCommentsByUserQueryResponse
            {
                TotalCommentsCount = totalCommentsCount,
                Comments = comments
            };     
        }
    }
}
