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
            await CreateTransactionsTableAsync(connection);
            await CreateStockTableAsync(connection);
            await CreateErrorLogsTableAsync(connection);
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

        private static async Task CreateTransactionsTableAsync(IDbConnection connection)
        {
            string query = @"
                CREATE TABLE IF NOT EXISTS Transactions (
                    Id CHAR(36) NOT NULL,
                    ProductId CHAR(36) NOT NULL,
                    Quantity INT NOT NULL,
                    TransactionDate DATETIME NOT NULL,
                    TransactionType VARCHAR(50) NOT NULL,
                    Cost DECIMAL(18, 2) NOT NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (ProductId) REFERENCES Products(Id)
                )";

            await connection.ExecuteAsync(query);
        }

        private static async Task CreateStockTableAsync(IDbConnection connection)
        {
            string query = @"
                CREATE TABLE IF NOT EXISTS Stock (
                    Id CHAR(36) NOT NULL,
                    ProductId CHAR(36) NOT NULL,
                    Quantity INT NOT NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (ProductId) REFERENCES Products(Id)
                )";

            await connection.ExecuteAsync(query);
        }

        private static async Task CreateErrorLogsTableAsync(IDbConnection connection)
        {
            string query = @"
                CREATE TABLE IF NOT EXISTS ErrorLogs (
                    Id CHAR(36) NOT NULL,
                    CorrelationId CHAR(36) NOT NULL,
                    RequestType VARCHAR(255),
                    Request TEXT,
                    Exception TEXT,
                    Timestamp DATETIME NOT NULL,
                    PRIMARY KEY (Id)
                )";
            await connection.ExecuteAsync(query);
        }
    }
}
