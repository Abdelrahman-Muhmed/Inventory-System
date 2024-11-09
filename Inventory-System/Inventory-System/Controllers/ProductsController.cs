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
            try
            {
                var getProducts = await _productService.GetAllProductAsync();
                if (getProducts == null)
                    return NotFound("No products found.");

                var data = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductRetuenDto>>(getProducts);

                return Ok(data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing request" + ex);
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRetuenDto>> GetProductAsync(int id)
        {
            try
            {
                var product = await _productService.GetProductAsync(id);
                if (product == null)
                    return NotFound("No product with this id found");

                var ProductMapp = _mapper.Map<Products, ProductRetuenDto>(product);
                return Ok(ProductMapp);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing request" + ex);
            }

        }

        [HttpGet("getProductsByCategoryName")]
        public async Task<ActionResult<ProductRetuenDto>> getProductsByCategoryName(string CategoryName)
        {
            try
            {
                var ProductsByCategoryName = await _productService.GetProductsByBrandName(CategoryName);
                if (ProductsByCategoryName == null)
                    return NotFound("No Category with this Nmae found");

                return Ok(ProductsByCategoryName);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "An error occurred while processing request" + ex);
            }
        }

        [HttpGet("getBrand")]
        public async Task<ActionResult<ProductBrand>> getBroductPrand()
        {
            var brands = await _productService.GetProductBrandAsync();
            if(brands == null)
                return NotFound("No Brands found");

            return Ok(brands);
        }

        [HttpGet("getCategory")]
        public async Task<ActionResult<ProductBrand>> getProductCategory()
        {
            try
            {
                var categors = await _productService.GetProductCategoryAsync();
                if (categors == null)
                    return NotFound("No categors found");

                return Ok(categors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "An error occurred while processing request" + ex);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ProductRetuenDto>> CreateProduct(Products products)
        {
            try
            {
                if (products == null)
                    return BadRequest("Product data cannot be null.");
                var CreateProducts = await _productService.Add(products);

                _mapper.Map<ProductRetuenDto>(CreateProducts);
                return Ok("Done");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                             "An error occurred while processing request" + ex);
            }

        }

        [HttpPut]
        public async Task<ActionResult<ProductRetuenDto>> UpdateProduct(Products products)
        {
            try
            {
                if (products == null)
                    return BadRequest("Product data cannot be null.");

                var updateProduct = await _productService.Update(products);
                _mapper.Map<ProductRetuenDto>(updateProduct);
                return Ok("Done");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                             "An error occurred while processing request" + ex);
            }

        }
    }
}
