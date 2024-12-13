using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStockAsync();

        Task<Stock?> GetStockByIdAsync(Guid id);

        Task AddStockAsync(Stock stock);

        Task UpdateStockAsync(Stock stock);

        Task DeleteStockAsync(Guid id);
    }
}
