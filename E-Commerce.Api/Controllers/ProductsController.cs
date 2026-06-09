using E_Commerce.Core.Entities;
using E_Commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await context.Products.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExist(id))
            {
                return BadRequest("Product cannot be update");
            }

            //Generate an UPDATE statement for this entire object even though you didn't load it from the database.
            context.Entry(product).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(product);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound();

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return $"{product.Name} deleted Successefully";

        }

        private bool ProductExist(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }
    }
}
