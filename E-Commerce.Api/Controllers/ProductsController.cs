using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repository) : BaseApiController
    {


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            return await CreatePageResult(repository, spec, specParams.PageIndex, specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repository.Add(product);

            if (await repository.SaveChangesAsync())
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

            if (!await repository.ExistsAsync(id))
            {
                return NotFound();
            }

            //update all the product
            //Generate an UPDATE statement for this entire object even though you didn't load it from the database.
            repository.Update(product);

            if (await repository.SaveChangesAsync())
            {
                return Ok(product);

            }

            return BadRequest("Problem when updating");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteProduct(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product == null) return NotFound();

            repository.Delete(product);
            if (await repository.SaveChangesAsync())
            {
                return $"{product.Name} deleted Successefully";
            }
            return BadRequest("Could not delete this product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await repository.ListAsync(spec));
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repository.ListAsync(spec));
        }
    }
}
