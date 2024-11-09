using Inventory_System_Core.IRepository;
using Inventory_System_Core.Model;
using Inventory_System_EF.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System_EF.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _reopsitory; //It's Like Dictionary<string, GenericRepository<BaseEntity>> _reopsitory;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _reopsitory = new Hashtable();
        }
     

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            //I can path the object and equal it there but that will make me add for every object and make equal in the Feuture So i will using Dictionary 


            //I need to get the key and Value
            var key = typeof(T).Name;

            if (!_reopsitory.ContainsKey(key))
            {
                //var value = new GenericRepository<T>(_dbContext) as GenericRepository<BaseEntity>;
                var value = new GenericRepository<T>(_dbContext);

                _reopsitory.Add(key, value);
            }

            return _reopsitory[key] as IGenericRepository<T>;

        }


        public async Task<int> CompleteAsync()
         => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
         => await _dbContext.DisposeAsync();
    }
}
