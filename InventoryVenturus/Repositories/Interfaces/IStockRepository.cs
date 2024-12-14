using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStockAsync();

        Task<Stock?> GetStockByProductIdAsync(Guid productId);

        Task AddStockAsync(Stock stock);

        Task<bool> UpdateStockAsync(Stock stock);

        Task<bool> DeleteStockAsync(Guid id);
    }
}
