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
            //For Include Category and Product Name <===
            //if (typeof(T) == typeof(Product))
            //    return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p => p.ProductBrand).Include(p => p.CategoryName).ToListAsync();
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //    return await _dbContext.Set<Product>().Where(p => p.id == id).Include(p => p.ProductBrand).Include(p => p.CategoryName).FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }


        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);

        public void Add(T entity)
       => _dbContext.Set<T>().Add(entity);

    }
}
