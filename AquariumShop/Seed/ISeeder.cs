using Domain.Models;

namespace AquariumShop.Seed
{
    public interface ISeeder<T> where T : BaseModel
    {

        public Task SeedAsync();
    }
}
