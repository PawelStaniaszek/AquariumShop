using AquariumShop.Commands;
using AquariumShop.Dtos;
using AquariumShop.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AquariumShop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        
        public CartController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IEnumerable<ProductForCartDto>> GetCartByUser()
        {
            var userId = this.GetUserGuidFromRequest();

            if (userId == null)
            {
                return (IEnumerable<ProductForCartDto>)BadRequest("User doesn't exist");
            }
            else
            {
                var query = new GetCartByUserQuery();
                query.UserId = userId;
                return await _mediator.Send(query);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartCommand command)
        {
            var userId = this.GetUserGuidFromRequest();

            if (userId == null)
            {
                return BadRequest("User doesn't exist");
            }
            else
            {
                command.UserId = userId;
                return await _mediator.Send(command);
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromCart([FromQuery] Guid productId)
        {
            var userId = this.GetUserGuidFromRequest();
            if(userId == null)
            {
                return BadRequest("User doesn't exist");
            }
            else
            {
                var command = new DeleteProductFromCartCommand();
                command.UserId = userId;
                command.ProductId = productId;
                return await _mediator.Send(command);
            }
        }
    }
}
