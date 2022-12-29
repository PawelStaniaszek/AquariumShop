using AquariumShop.Dtos;
using AquariumShop.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            return await _mediator.Send(new GetAllProductsQuery());
        }

        [Route("/Category")]
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductsByCategory([FromQuery]Guid categoryId)
        {
            return await _mediator.Send(new GetProductsByCategoryQuery { CategoryId =  categoryId });
        }

    }
}
