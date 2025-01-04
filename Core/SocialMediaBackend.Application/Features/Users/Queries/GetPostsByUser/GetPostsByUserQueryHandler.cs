﻿using MediatR;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Posts;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryHandler : IRequestHandler<GetPostsByUserQueryRequest, GetPostsByUserQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IConfiguration _configuration;

        public GetPostsByUserQueryHandler(IPostReadRepository postReadRepository, IConfiguration configuration)
        {
            _postReadRepository = postReadRepository;
            _configuration = configuration;
        }

        public async Task<GetPostsByUserQueryResponse> Handle(GetPostsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            int totalPostCount = _postReadRepository.GetWhere(p => p.AppUser.UserName == request.UserName).Count();

            var posts = _postReadRepository.GetAll(false)
                .Where(p => p.AppUser.UserName == request.UserName)
                .OrderByDescending(p => p.CreatedDate)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
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
                    LikeCount = p.LikeCount,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    PostImages = p.PostImages.Select(pi => new PostImagesDto { Path = _configuration["StorageUrls:LocalStorage"] + pi.Path }).ToList()
                });

            return new GetPostsByUserQueryResponse { TotalPostCount = totalPostCount, Posts = posts };
        }
    }
}
