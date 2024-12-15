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

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(DateTime date)
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Transactions WHERE CAST(TransactionDate AS DATE) = @Date";
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
    }
}
