using AutoMapper;
using Inventory_System.Dtos;
using Inventory_System_Core.Model;
using Inventory_System_Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController( IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;

        }

        [HttpGet]
        public async Task<ActionResult<ProductRetuenDto>> GetAllAsync()
        {
            var getProducts = await _productService.GetAllProductAsync();
            var data = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductRetuenDto>>(getProducts);
            
            return Ok(data);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRetuenDto>> GetProductAsync(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound();
            var ProductMapp = _mapper.Map<Products, ProductRetuenDto>(product);
            return Ok(ProductMapp);

        }

        [HttpGet("getBrand")]
        public async Task<ActionResult<ProductBrand>> getBroductPrand()
        {
            var brands = await _productService.GetProductBrandAsync();
            return Ok(brands);
        }
        [HttpGet("getCategory")]
        public async Task<ActionResult<ProductBrand>> getProductCategory()
        {
            var categors = await _productService.GetProductCategoryAsync();
            return Ok(categors);

        }
    }
}
