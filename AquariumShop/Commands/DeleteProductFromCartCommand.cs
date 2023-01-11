using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquariumShop.Commands
{
    public class DeleteProductFromCartCommand : IRequest<IActionResult>
    {
        [Required]
        public string? UserId { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}
