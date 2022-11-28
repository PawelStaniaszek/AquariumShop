using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly AquariumDbContext db;
        public DbSet<Product> ObjectSet { get; }
        public ProductRepository(AquariumDbContext _db)
        {
            db = _db;
            ObjectSet = db.Set<Product>();
        }

        public Task<IActionResult> Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllIncluding(Expression<Func<Product, object>>[] include)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetById(Guid id)
        {
            var result = await ObjectSet.Where(a => a.CategoryId.Equals(id)).ToListAsync();
            return result;
        }

        public Task<Product> GetDetail(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
