using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(Guid id);

        Task AddProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> UpdateAveragePriceAsync(Guid id, decimal price);

        Task<bool> DeleteProductAsync(Guid id);
    }
}
