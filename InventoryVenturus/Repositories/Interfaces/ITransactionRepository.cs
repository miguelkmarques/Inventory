using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(DateTime date);

        Task AddTransactionAsync(Transaction transaction);
    }
}
