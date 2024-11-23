using MediatR;
using SocialMediaBackend.Application.DTOs.Categories;
using SocialMediaBackend.Application.Repositories.Categories;

namespace SocialMediaBackend.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;

        public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = _categoryReadRepository.GetAll(false).Select(x => new ListCategoryDto()
            {
                Id = x.Id,
                Title = x.Title,
            });

            return new GetAllCategoryQueryResponse()
            {
                Succeeded = true,
                Categories = categories
            };
        }
    }
}
