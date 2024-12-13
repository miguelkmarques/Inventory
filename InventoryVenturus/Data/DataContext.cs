using Dapper;
using InventoryVenturus.Data.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace InventoryVenturus.Data
{
    public class DataContext(IConfiguration configuration) : IDataContext
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("DefaultConnection not set");

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task InitDatabase()
        {
            using var connection = CreateConnection();
            connection.Open();
            await CreateProductsTableAsync(connection);
        }

        private static async Task CreateProductsTableAsync(IDbConnection connection)
        {
            string query = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id CHAR(36) NOT NULL,
                    Partnumber VARCHAR(255) NOT NULL,
                    Name VARCHAR(255) NOT NULL,
                    Price DECIMAL(18, 2) NOT NULL,
                    PRIMARY KEY (Id)
                )";
            await connection.ExecuteAsync(query);
        }
    }
}
