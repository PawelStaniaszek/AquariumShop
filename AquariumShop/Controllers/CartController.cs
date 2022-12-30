using AquariumShop.Commands;
using AquariumShop.Dtos;
using AquariumShop.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquariumShop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        
        public CartController(IMediator mediator): base(mediator) { }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetCartByUser()
        {
            var userId = this.GetUserGuidFromRequest();

            if (userId == null)
            {
                return (IEnumerable<ProductDto>)BadRequest("User doesn't exist");
            }
            else
            {
                var query = new GetCartByUserQuery();
                query.UserId = userId;
                return await _mediator.Send(query);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromQuery] Guid productId)
        {
            var userId = this.GetUserGuidFromRequest();

            if (userId == null)
            {
                return BadRequest("User doesn't exist");
            }
            else
            {
                var command = new AddProductToCartCommand();
                command.ProductId = productId;
                command.UserId = userId;
                return await _mediator.Send(command);
            }
            
        }
    }
}
