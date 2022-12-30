using Domain.Models;
using Infrastructure.Repository;

namespace AquariumShop.Seed
{
    public class ProductSeeder : ISeeder<Product>
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Category> categoryRepository;

        public ProductSeeder(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task SeedAsync()
        {
            var existingProducts = await productRepository.GetAll();
            var existingCategories = await categoryRepository.GetAll();
            if(!existingProducts.Any() && existingCategories.Any())
            {
                await productRepository.AddAsync(new Product() { Id = Guid.NewGuid(), Name = "Produkt", CategoryId = existingCategories.ElementAt(0).Id, Price = 123});
                await productRepository.AddAsync(new Product() { Id = Guid.NewGuid(), Name = "Produkt1", CategoryId = existingCategories.ElementAt(0).Id, Price = 213 });
                await productRepository.AddAsync(new Product() { Id = Guid.NewGuid(), Name = "Produkt2", CategoryId = existingCategories.ElementAt(2).Id, Price = 123 });
            }
        }
    }
}
