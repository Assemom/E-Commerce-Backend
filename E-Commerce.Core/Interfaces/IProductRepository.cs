using E_Commerce.Core.Entities;

namespace E_Commerce.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
        Task<Product?> GetProductAsync(int id);
        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<bool> ProductExistsAsync(int id);
        Task<bool> SaveChangesAsync();

    }
}
