using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetConsumptionTransactionsByDateAsync(DateTime date);

        Task AddTransactionAsync(Transaction transaction);

        Task<int> DeleteProductTransactionsAsync(Guid productId);
    }
}
