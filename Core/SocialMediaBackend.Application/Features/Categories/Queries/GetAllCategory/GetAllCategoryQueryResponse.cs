using SocialMediaBackend.Application.DTOs.Categories;

namespace SocialMediaBackend.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryResponse
    {
        public IQueryable<ListCategoryDto> Categories { get; set; }
    }
}
