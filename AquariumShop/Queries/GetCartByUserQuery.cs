using AquariumShop.Dtos;
using MediatR;

namespace AquariumShop.Queries
{
    public class GetCartByUserQuery : IRequest<IEnumerable<ProductForCartDto>>
    {
        public string UserId { get; set; }
    }
}
