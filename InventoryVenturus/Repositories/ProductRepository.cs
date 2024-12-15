using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using Dapper;
using System.Data;
using InventoryVenturus.Data.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace InventoryVenturus.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ProductRepository(IDataContext dataContext) : IProductRepository
    {
        private IDbConnection Connection => dataContext.CreateConnection();

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Products";
            dbConnection.Open();
            return await dbConnection.QueryAsync<Product>(query);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Products WHERE Id = @Id";
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task AddProductAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            string query = "INSERT INTO Products (Id, Partnumber, Name, Price) VALUES (@Id, @Partnumber, @Name, @Price)";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            string query = "UPDATE Products SET Partnumber = @Partnumber, Name = @Name WHERE Id = @Id";
            dbConnection.Open();
            var rowsAffected = await dbConnection.ExecuteAsync(query, product);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAveragePriceAsync(Guid id, decimal price)
        {
            using IDbConnection dbConnection = Connection;
            string query = "UPDATE Products SET Price = @Price WHERE Id = @Id";
            dbConnection.Open();
            var rowsAffected = await dbConnection.ExecuteAsync(query, new { Id = id, Price = price });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            string query = "DELETE FROM Products WHERE Id = @Id";
            dbConnection.Open();
            var rowsAffected = await dbConnection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
