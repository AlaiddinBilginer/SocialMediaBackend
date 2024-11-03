using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public CategoriesController(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
        {
            _categoryWriteRepository = categoryWriteRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create()
        {
            await _categoryWriteRepository.AddAsync(new Category()
            {
                Title = "Film"
            });
            await _categoryWriteRepository.SaveAsync();
            return Ok("Kategori ekleme işlemi başarılı");
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            Category category = await _categoryReadRepository.GetByIdAsync(id);
            return Ok(category);
        }
    }
}
