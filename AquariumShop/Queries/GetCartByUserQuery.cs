using AquariumShop.Dtos;
using MediatR;

namespace AquariumShop.Queries
{
    public class GetCartByUserQuery : IRequest<IEnumerable<ProductDto>>
    {
        public string UserId { get; set; }
    }
}
