using E_Commerce.Core.Entities;
using System.Text.Json;

namespace E_Commerce.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var productdata = await File.ReadAllTextAsync("../E-Commerce.Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productdata);

                if (products == null) return;

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }
        }
    }
}
