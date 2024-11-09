using Inventory_System_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System_Core.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;  // So when i path any Table here he will create The Object What i need When i ask 
        Task<int> CompleteAsync();
   

    }
}
