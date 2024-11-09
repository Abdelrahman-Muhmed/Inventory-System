using Inventory_System_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System_Core.Service
{
    public interface IProductService
    {
        Task<IReadOnlyList<Products>> GetAllProductAsync();
        Task<Products> GetProductAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
        Task<IEnumerable<Products>> GetProductsByBrandName(string CategoryName);

        Task<IReadOnlyList<ProductCategory>> GetProductCategoryAsync();
        Task<Products> Add(Products entity);
        Task<Products> Update(Products entity);
    }
}
