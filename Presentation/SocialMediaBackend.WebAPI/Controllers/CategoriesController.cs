using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Categories.Commands.CreateCategory;
using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public CategoriesController(ICategoryReadRepository categoryReadRepository, IMediator mediator)
        {
            _categoryReadRepository = categoryReadRepository;
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            Category category = await _categoryReadRepository.GetByIdAsync(id);
            return Ok(category);
        }
    }
}
