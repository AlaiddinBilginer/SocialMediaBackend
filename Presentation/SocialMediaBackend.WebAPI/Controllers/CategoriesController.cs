using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Categories.Commands.CreateCategory;
using SocialMediaBackend.Application.Features.Categories.Queries.GetAllCategory;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllCategoryQueryRequest();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
