using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository productRepository = productRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(string? brand, string? type, string? sort)
        {
            return Ok(await productRepository.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await productRepository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            productRepository.AddProduct(product);

            if (await productRepository.SaveChangesAsync())
            {
                return CreatedAtAction("GetProductById", new { id = product.Id }, product);
            }
            return BadRequest("Problem Creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {

            if (product.Id != id)
            {
                return BadRequest("Product cannot be update");
            }

            if (!await productRepository.ProductExistsAsync(id))
            {
                return NotFound();
            }

            //update all the product
            //Generate an UPDATE statement for this entire object even though you didn't load it from the database.
            productRepository.UpdateProduct(product);

            if (await productRepository.SaveChangesAsync())
            {
                return Ok(product);

            }

            return BadRequest("Problem when updating");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteProduct(int id)
        {
            var product = await productRepository.GetProductAsync(id);

            if (product == null) return NotFound();

            productRepository.DeleteProduct(product);
            if (await productRepository.SaveChangesAsync())
            {
                return $"{product.Name} deleted Successefully";
            }
            return BadRequest("Could not delete this product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await productRepository.GetBrandsAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await productRepository.GetTypesAsync());
        }
    }
}
