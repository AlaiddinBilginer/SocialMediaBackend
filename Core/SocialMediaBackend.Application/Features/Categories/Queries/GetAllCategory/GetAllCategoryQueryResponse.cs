using SocialMediaBackend.Application.DTOs.Categories;

namespace SocialMediaBackend.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryResponse
    {
        public bool Succeeded { get; set; }
        public IQueryable<ListCategoryDto> Categories { get; set; }
    }
}
