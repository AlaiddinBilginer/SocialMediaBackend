using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Application.Repositories.PostImages;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Application.Services.Storage;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
    {
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStorageService _storageService;
        private readonly IPostImageWriteRepository _postImageWriteRepository;

        public CreatePostCommandHandler(
            IPostWriteRepository postWriteRepository,
            IHttpContextAccessor contextAccessor,
            UserManager<AppUser> userManager,
            ICategoryReadRepository categoryReadRepository,
            IStorageService storageService,
            IPostImageWriteRepository postImageWriteRepository)
        {
            _postWriteRepository = postWriteRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _categoryReadRepository = categoryReadRepository;
            _storageService = storageService;
            _postImageWriteRepository = postImageWriteRepository;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            PostCategory category = await _categoryReadRepository.GetByIdAsync(request.CategoryId);

            if (user == null || category == null)
                return new CreatePostCommandResponse() { Succeeded = false, Message = "Gönderi yüklenirken hata ile karşılaşıldı" };

            Guid postId = Guid.NewGuid();

            await _postWriteRepository.AddAsync(new Post()
            {
                Id = postId,
                Title = request.Title,
                Content = request.Content,
                AppUserId = user.Id,
                CategoryId = category.Id,
            });
            await _postWriteRepository.SaveAsync();

            user.PostsCount++;
            await _userManager.UpdateAsync(user);

            if(request.Files != null)
            {
                var datas = await _storageService.UploadAsync("resource/post-images", request.Files);
                await _postImageWriteRepository.AddRangeAsync(datas.Select(d => new PostImage()
                {
                    FileName = d.fileName,
                    Path = d.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    PostId = postId
                }).ToList());
                await _postImageWriteRepository.SaveAsync();
            }

            return new CreatePostCommandResponse() { Succeeded = true, Message = "Gönderi başarılı bir şekilde yüklendi" };
        }
    }
}
