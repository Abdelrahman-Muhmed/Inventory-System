using Inventory_System_Core.IRepository;
using Inventory_System_Core.Model;
using Inventory_System_EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System_EF.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;

        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            // Check if T is of type Products
            if (typeof(T) == typeof(Products))
            {
                var products = await _dbContext.Set<Products>()
                    .Include(p => p.ProductBrand)
                    .Include(p => p.CategoryName)
                    .ToListAsync();
                return (IReadOnlyList<T>)(IReadOnlyList<Products>)products;
            }

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Products))
                return await _dbContext.Set<Products>()
                    .Where(p => p.id == id)
                    .Include(p => p.ProductBrand)
                    .Include(p => p.CategoryName)
                    .FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(id);
        }


        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);

        public void Add(T entity)
       => _dbContext.Set<T>().Add(entity);

        public async Task<IEnumerable<Products>> GetByBrandName(string CategoryName)
        {
            return await _dbContext.Product  
              .Where(p => p.CategoryName.Name == CategoryName)
              .Include(p => p.ProductBrand).Include(p => p.CategoryName)// Filter by BrandName
              .ToListAsync();
        }
    }
}
