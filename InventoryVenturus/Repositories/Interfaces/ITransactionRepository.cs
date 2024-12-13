using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        Task<Transaction?> GetTransactionByIdAsync(Guid id);

        Task AddTransactionAsync(Transaction transaction);

        Task UpdateTransactionAsync(Transaction transaction);

        Task DeleteTransactionAsync(Guid id);
    }
}
