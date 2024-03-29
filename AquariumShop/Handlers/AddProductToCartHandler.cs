﻿using AquariumShop.Commands;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Handlers
{
    public class AddProductToCartHandler : IRequestHandler<AddProductToCartCommand, IActionResult>
    {
        private readonly IRepository<Cart> _cartRepository;

        public AddProductToCartHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart()
            {
                Id = new Guid(),
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };
            var result = await _cartRepository.AddAsync(cart);
            if(result == 1)
            {
                return new OkResult();
            }else return new BadRequestObjectResult(result);
            
        }
    }
}
