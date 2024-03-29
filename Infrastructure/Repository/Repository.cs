﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddAsync(T entity)
        {
            await _objectSet.AddAsync(entity);
            
            return await db.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
            db.SaveChangesAsync();
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

        public async Task<T> GetById(Guid id)
        {
            var result = await ObjectSet.FirstOrDefaultAsync(a => a.Id == id);
            return result;
        }
    }
}
