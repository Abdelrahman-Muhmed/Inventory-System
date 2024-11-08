using Inventory_System_Core.IRepository;
using Inventory_System_Core.Model;
using Inventory_System_Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System_EF.Service
{
    public class ProductServic : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IReadOnlyList<Products>> GetAllProductAsync()
        {
            var products = _unitOfWork.Repository<Products>().GetAllAsync();
            return products;
        }

        public Task<Products> GetProductAsync(int id)
        {
            var product = _unitOfWork.Repository<Products>().GetAsync(id);
            return product;
        }

        public Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            var brands = _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return brands;
        }

        public Task<IReadOnlyList<ProductCategory>> GetProductCategoryAsync()
        {
            var category = _unitOfWork.Repository<ProductCategory>().GetAllAsync();
            return category;
        }
    }
}
