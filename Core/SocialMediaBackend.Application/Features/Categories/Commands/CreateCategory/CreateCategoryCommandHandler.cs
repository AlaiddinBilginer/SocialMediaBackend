using MediatR;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        private readonly ICategoryWriteRepository _categoryWriteRepository;

        public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _categoryWriteRepository.AddAsync(new PostCategory() { Title = request.Title, Photo = request.Photo });
            await _categoryWriteRepository.SaveAsync();

            return new CreateCategoryCommandSuccessResponse() { Message = "Kategori eklendi" };
        }
    }
}
