using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Application.Repositories.Posts;
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

        public CreatePostCommandHandler(
            IPostWriteRepository postWriteRepository, 
            IHttpContextAccessor contextAccessor, 
            UserManager<AppUser> userManager, 
            ICategoryReadRepository categoryReadRepository)
        {
            _postWriteRepository = postWriteRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            Category category = await _categoryReadRepository.GetByIdAsync(request.CategoryId);

            if (user == null || category == null)
                return new CreatePostCommandResponse() { Succeeded = false, Message = "Gönderi yüklenirken hata ile karşılaşıldı" };

            await _postWriteRepository.AddAsync(new Post()
            {
                Title = request.Title,
                Content = request.Content,
                AppUserId = user.Id,
                CategoryId = category.Id,
            });
            await _postWriteRepository.SaveAsync();

            return new CreatePostCommandResponse() { Succeeded = true, Message = "Gönderi başarılı bir şekilde yüklendi" };
        }
    }
}
