using AquariumShop.Dtos;
using AquariumShop.Queries;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;

namespace AquariumShop.Handlers
{
    public class GetCartByUserHandler : IRequestHandler<GetCartByUserQuery, IEnumerable<ProductDto>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetCartByUserHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<ProductDto>> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
