﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Tags;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetByIdPost
{
    public class GetByIdPostQueryHandler : IRequestHandler<GetByIdPostQueryRequest, GetByIdPostQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public GetByIdPostQueryHandler(IPostReadRepository postReadRepository, IConfiguration configuration, ICurrentUserService currentUserService)
        {
            _postReadRepository = postReadRepository;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }

        public async Task<GetByIdPostQueryResponse> Handle(GetByIdPostQueryRequest request, CancellationToken cancellationToken)
        {
            var post = await _postReadRepository.GetWhere(p => p.Id == Guid.Parse(request.Id))
                .Include(p => p.PostImages)
                .Include(p => p.AppUser)
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .Include(p => p.Likes)
                .Select(p => new GetByIdPostQueryResponse
                {
                    Id = p.Id,
                    Content = p.Content,
                    Title = p.Title,
                    CategoryId = p.CategoryId.ToString(),
                    CategoryName = p.Category.Title,
                    UserId = p.AppUserId,
                    UserName = p.AppUser.UserName,
                    UserProfilePhoto = p.AppUser.ProfilePhoto,
                    IsLiked = p.Likes.Where(x => x.AppUserId == _currentUserService.UserId).Any(),
                    LikeCount = p.LikeCount,
                    CommentCount = p.CommentCount,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    Tags = p.Tags.Select(t => new TagDto { Id = t.Id, Title = t.Title }).ToList(),
                    PostImages = p.PostImages.Select(pi => new PostImagesDto { Path = _configuration["StorageUrls:LocalStorage"] + pi.Path }).ToList(),
                }).FirstOrDefaultAsync();

            return post;
        }
    }
}
