using Domain.Models;

namespace AquariumShop.Dtos
{
    public class ProductForCartDto
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
