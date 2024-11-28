using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Posts;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQueryHandler : IRequestHandler<GetPostsByCategoryQueryRequest, GetPostsByCategoryQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IConfiguration _configuration;

        public GetPostsByCategoryQueryHandler(IPostReadRepository postReadRepository, IConfiguration configuration)
        {
            _postReadRepository = postReadRepository;
            _configuration = configuration;
        }

        public async Task<GetPostsByCategoryQueryResponse> Handle(GetPostsByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            int totalPostCoynt = _postReadRepository.GetWhere(p => p.Category.Title == request.CategoryName, false).Count();

            var posts = _postReadRepository.GetWhere(p => p.Category.Title == request.CategoryName, false)
                .OrderByDescending(p => p.CreatedDate)
                .Skip(request.Pagination.Size * request.Pagination.Page)
                .Take(request.Pagination.Size)
                .Include(p => p.AppUser)
                .Include(p => p.PostImages)
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
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    PostImages = p.PostImages.Select(pi => new PostImagesDto { Path = _configuration["StorageUrls:LocalStorage"] + pi.Path }).ToList()
                });
            
            return new GetPostsByCategoryQueryResponse() { TotalPostCount = totalPostCoynt, Posts = posts, };

        }
    }
}
