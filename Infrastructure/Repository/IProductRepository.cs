using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    
        public interface IProductRepository<T> where T : Product
        {
            public DbSet<T> ObjectSet { get; }
            Task<IEnumerable<T>> GetAll();
            Task<IActionResult> Add(T entity);
            void Delete(T entity);
            void Edit(T entity);
            Task<IEnumerable<T>> GetById(Guid id);
        }
    
}
