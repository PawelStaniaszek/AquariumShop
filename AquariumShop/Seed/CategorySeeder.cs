using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace AquariumShop.Seed
{
    public class CategorySeeder : ISeeder<Category>
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategorySeeder(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task SeedAsync()
        {
            var existingCategory = await _categoryRepository.GetAll();
            if (!existingCategory.Any())
            {
                await _categoryRepository.AddAsync(new Category { Id = Guid.NewGuid(), Name = "Akcesoria" });
                await _categoryRepository.AddAsync(new Category { Id = Guid.NewGuid(), Name = "Zestawy" });
                await _categoryRepository.AddAsync(new Category { Id = Guid.NewGuid(), Name = "Akwaria" });
                await _categoryRepository.AddAsync(new Category { Id = Guid.NewGuid(), Name = "Rośliny" });
            }
        }
    }
}
