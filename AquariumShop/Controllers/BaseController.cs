using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquariumShop.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected string? GetUserGuidFromRequest()
        {
            return this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
