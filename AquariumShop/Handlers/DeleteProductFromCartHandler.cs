using AquariumShop.Commands;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AquariumShop.Handlers
{
    public class DeleteProductFromCartHandler : IRequestHandler<DeleteProductFromCartCommand, IActionResult>
    {
        private readonly IRepository<Cart> _cartRepository;

        public DeleteProductFromCartHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<IActionResult> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.ObjectSet.FirstOrDefaultAsync<Cart>(x => x.ProductId == request.ProductId && x.UserId == request.UserId);
            _cartRepository.Delete(cart);
            return new OkResult();
        }
    }
}
