using AquariumShop.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}
