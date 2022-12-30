using AquariumShop.Dtos;
using AquariumShop.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {

        public CategoryController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get()
        {
            return await _mediator.Send(new GetAllCategoriesQuery());
        }
    }
}
