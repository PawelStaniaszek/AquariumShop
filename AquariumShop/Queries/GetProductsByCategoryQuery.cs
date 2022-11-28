using AquariumShop.Dtos;
using MediatR;

namespace AquariumShop.Queries
{
    public class GetProductsByCategoryQuery : IRequest<IEnumerable<ProductDto>>
    {
        public Guid CategoryId { get; set; }
    }
}
