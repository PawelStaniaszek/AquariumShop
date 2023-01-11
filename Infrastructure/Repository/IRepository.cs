using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        public DbSet<T> ObjectSet { get; }
        Task<IEnumerable<T>> GetAll();
        Task<int> AddAsync(T entity);
        void Delete(T entity);
        void Edit(T entity);
        Task<T> GetById(Guid id);
    }
}
