using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByProductIdAsync(Guid productId);

        Task AddStockAsync(Stock stock);

        Task<bool> UpdateStockAsync(Stock stock);

        Task<bool> DeleteStockAsync(Guid id);
    }
}
