using AquariumShop.Commands;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IRepository<Product> _productRepository;

        public AddProductHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                LongDescription = request.LongDescription,
                Picture = request.Picture,
                CategoryId = request.CategoryId,
                Id = new Guid()
            };
            return await _productRepository.AddAsync(product);
        }
    }
}
