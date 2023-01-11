using AquariumShop.Commands;
using AquariumShop.Dtos;
using AquariumShop.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {

        public ProductController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            return await _mediator.Send(new GetAllProductsQuery());
        }

        [Route("/api/[controller]/SingleProduct")]
        [HttpGet]
        public async Task<ProductDto> GetOne([FromQuery] Guid id)
        {
            var query = new GetProductQuery()
            {
                Id = id
            };
            return await _mediator.Send(query);
        }


        [HttpPost]
        public async Task<int> AddProduct([FromBody] AddProductCommand command)
        {
            return await _mediator.Send(command);
        }
        [Route("/CategoryName")]
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryName([FromQuery] string categoryName)
        {
            return await _mediator.Send(new GetProductsByCategoryNameQuery { Name = categoryName });
        }

        [Route("/Category")]
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductsByCategory([FromQuery] Guid categoryId)
        {
            return await _mediator.Send(new GetProductsByCategoryQuery { CategoryId = categoryId });
        }

    }
}
