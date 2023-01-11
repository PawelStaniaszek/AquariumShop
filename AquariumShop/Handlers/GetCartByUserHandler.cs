using AquariumShop.Dtos;
using AquariumShop.Queries;
using AutoMapper;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquariumShop.Handlers
{
    public class GetCartByUserHandler : IRequestHandler<GetCartByUserQuery, IEnumerable<ProductForCartDto>>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;

        public GetCartByUserHandler(IRepository<Cart> cartRepository, IRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            
            _productRepository = productRepository;
           
        }

        public async Task<IEnumerable<ProductForCartDto>> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
        {
            var query = _cartRepository.ObjectSet
                .Where(x => x.UserId == request.UserId)
                .GroupBy(x => x.Product.Id)
                .Select(z=> new { Ilosc = z.Sum(y => y.Quantity), product = z.Key }).AsEnumerable();

           var list = new List<ProductForCartDto>();
           foreach (var product in query)
           {
               var a = await _productRepository.GetById(product.product);
               var ilosc = query.FirstOrDefault(x => x.product == a.Id);
               list.Add(new ProductForCartDto()
               {
                   product = a,
                   Quantity = ilosc.Ilosc
               });
           }

            
            return list;
        }
    }
}
