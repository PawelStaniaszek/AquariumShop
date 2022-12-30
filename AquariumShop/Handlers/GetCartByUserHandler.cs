using AquariumShop.Dtos;
using AquariumShop.Queries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquariumShop.Handlers
{
    public class GetCartByUserHandler : IRequestHandler<GetCartByUserQuery, IEnumerable<ProductDto>>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;

        public GetCartByUserHandler(IRepository<Cart> cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
        {
            var query = _cartRepository.ObjectSet.Where(x => x.UserId == request.UserId).Include(x => x.Product);
            return _mapper.Map<IEnumerable<ProductDto>>(query);
        }
    }
}
