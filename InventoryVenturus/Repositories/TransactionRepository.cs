using Dapper;
using InventoryVenturus.Data.Interfaces;
using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace InventoryVenturus.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TransactionRepository(IDataContext dataContext) : ITransactionRepository
    {
        private IDbConnection Connection => dataContext.CreateConnection();

        public async Task<IEnumerable<Transaction>> GetConsumptionTransactionsByDateAsync(DateTime date)
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Transactions WHERE CAST(TransactionDate AS DATE) = @Date and TransactionType = 1";
            dbConnection.Open();
            return await dbConnection.QueryAsync<Transaction>(query, new { date.Date });
        }


        public async Task AddTransactionAsync(Transaction transaction)
        {
            using IDbConnection dbConnection = Connection;
            string query = "INSERT INTO Transactions (Id, ProductId, Quantity, TransactionDate, TransactionType, Cost) VALUES (@Id, @ProductId, @Quantity, @TransactionDate, @TransactionType, @Cost)";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, transaction);
        }

        public async Task<int> DeleteProductTransactionsAsync(Guid productId)
        {
            using IDbConnection dbConnection = Connection;
            string query = "DELETE FROM Transactions WHERE ProductId = @ProductId";
            dbConnection.Open();
            var rowsAffected = await dbConnection.ExecuteAsync(query, new { ProductId = productId });
            return rowsAffected;
        }
    }
}
