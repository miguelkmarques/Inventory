﻿using Dapper;
using InventoryVenturus.Data.Interfaces;
using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace InventoryVenturus.Repositories
{
    [ExcludeFromCodeCoverage]
    public class StockRepository(IDataContext dataContext) : IStockRepository
    {
        private IDbConnection Connection => dataContext.CreateConnection();

        public async Task<Stock?> GetStockByProductIdAsync(Guid productId)
        {
            using IDbConnection dbConnection = Connection;
            string query = "SELECT * FROM Stock WHERE ProductId = @ProductId";
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Stock>(query, new { ProductId = productId });
        }

        public async Task AddStockAsync(Stock stock)
        {
            using IDbConnection dbConnection = Connection;
            string query = "INSERT INTO Stock (ProductId, Quantity) VALUES (@ProductId, @Quantity)";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, stock);
        }

        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            using IDbConnection dbConnection = Connection;
            string query = "UPDATE Stock SET Quantity = @Quantity WHERE ProductId = @ProductId";
            dbConnection.Open();
            var rowsAffected = await dbConnection.ExecuteAsync(query, stock);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteStockAsync(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            string query = "DELETE FROM Stock WHERE ProductId = @ProductId";
            dbConnection.Open();
            var rowsAffected = await dbConnection.ExecuteAsync(query, new { ProductId = id });
            return rowsAffected > 0;
        }
    }
}
