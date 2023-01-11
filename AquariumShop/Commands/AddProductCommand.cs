using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquariumShop.Commands
{
    public class AddProductCommand : IRequest<int>
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Price { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string Picture { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }
    }
}
