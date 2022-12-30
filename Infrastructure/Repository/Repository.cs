using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly AquariumDbContext db;
        private readonly DbSet<T> _objectSet;
        public Repository(AquariumDbContext _db)
        {
            db = _db;
            _objectSet = db.Set<T>();
        }

        public DbSet<T> ObjectSet
        {
            get { return _objectSet; }
        }

        public async Task<IActionResult> AddAsync(T entity)
        {
            var result = await _objectSet.AddAsync(entity);
            await db.SaveChangesAsync();
            return (IActionResult)result;
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            _objectSet.Update(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _objectSet.ToListAsync();
        }

        public async Task<T> GetDetail(Expression<Func<T, bool>> predicate)
        {
            return await _objectSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllIncluding(Expression<Func<T, object>>[]include)
        {
            var result = _objectSet.AsQueryable();
            var query = include.Aggregate(result, (current, include) => current.Include(include));
            return (IEnumerable<T>)query;
        }

        public async Task<IEnumerable<T>> GetById(Guid id)
        {
            var result = await ObjectSet.Where(a => a.Id == id).ToListAsync();
            return result;
        }
    }
}
