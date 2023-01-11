using AquariumShop.Dtos;
using MediatR;

namespace AquariumShop.Queries
{
    public class GetProductsByCategoryNameQuery : IRequest<IEnumerable<ProductDto>>
    {
        public string Name { get; set; }
    }
}
