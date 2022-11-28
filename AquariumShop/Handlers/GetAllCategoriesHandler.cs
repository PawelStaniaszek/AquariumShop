using AquariumShop.Dtos;
using AquariumShop.Queries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;

namespace AquariumShop.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(query);
        }
    }
}
