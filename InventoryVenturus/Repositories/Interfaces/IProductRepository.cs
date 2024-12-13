using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(Guid id);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(Guid id);
    }
}
