using AquariumShop.Dtos;
using MediatR;

namespace AquariumShop.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
