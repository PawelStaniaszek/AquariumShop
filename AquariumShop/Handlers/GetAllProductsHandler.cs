﻿using AquariumShop.Dtos;
using AquariumShop.Queries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;

namespace AquariumShop.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDto>>(query);
        }
    }
}
