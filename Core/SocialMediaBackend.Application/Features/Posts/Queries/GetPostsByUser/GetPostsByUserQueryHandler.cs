using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Posts;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryHandler : IRequestHandler<GetPostsByUserQueryRequest, GetPostsByUserQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public GetPostsByUserQueryHandler(IPostReadRepository postReadRepository, IConfiguration configuration, ICurrentUserService currentUserService)
        {
            _postReadRepository = postReadRepository;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }

        public async Task<GetPostsByUserQueryResponse> Handle(GetPostsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            int totalPostCount = _postReadRepository.GetAll(false).Count();

            var posts = _postReadRepository.GetAll(false)
                .OrderByDescending(p => p.CreatedDate)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Include(p => p.AppUser)
                .Include(p => p.PostImages)
                .Include(p => p.Likes)
                .Select(p => new PostListDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Title,
                    UserId = p.AppUserId,
                    UserName = p.AppUser.UserName,
                    UserProfilePhoto = p.AppUser.ProfilePhoto,
                    IsLiked = p.Likes.Where(x => x.AppUserId == _currentUserService.UserId).Any(),
                    LikeCount = p.LikeCount,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    PostImages = p.PostImages.Select(pi => new PostImagesDto { Path = _configuration["StorageUrls:LocalStorage"] + pi.Path}).ToList()
                });

            return new GetPostsByUserQueryResponse() { TotalPostCount = totalPostCount, Posts = posts };
        }
    }
}
