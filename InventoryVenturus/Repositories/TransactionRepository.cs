using Dapper;
using InventoryVenturus.Data.Interfaces;
using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using System.Data;

namespace InventoryVenturus.Repositories
{
    public class TransactionRepository(IDataContext dataContext) : ITransactionRepository
    {
        private IDbConnection Connection => dataContext.CreateConnection();

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Transactions";
            dbConnection.Open();
            return await dbConnection.QueryAsync<Transaction>(query);
        }

        public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Transactions WHERE Id = @Id";
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Transaction>(query, new { Id = id });
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            using IDbConnection dbConnection = Connection;
            string query = "INSERT INTO Transactions (Id, ProductId, Quantity, TransactionDate, TransactionType, Cost) VALUES (@Id, @ProductId, @Quantity, @TransactionDate, @TransactionType, @Cost)";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            using IDbConnection dbConnection = Connection;
            string query = "UPDATE Transactions SET ProductId = @ProductId, Quantity = @Quantity, TransactionDate = @TransactionDate, TransactionType = @TransactionType, Cost = @Cost WHERE Id = @Id";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, transaction);
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            string query = "DELETE FROM Transactions WHERE Id = @Id";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
