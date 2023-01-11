using AquariumShop.Dtos;
using MediatR;

namespace AquariumShop.Queries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
