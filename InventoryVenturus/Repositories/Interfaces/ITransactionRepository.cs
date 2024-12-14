using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        Task<Transaction?> GetTransactionByIdAsync(Guid id);

        Task AddTransactionAsync(Transaction transaction);

        Task<bool> UpdateTransactionAsync(Transaction transaction);

        Task<bool> DeleteTransactionAsync(Guid id);
    }
}
