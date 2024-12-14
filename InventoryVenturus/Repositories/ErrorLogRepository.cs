using Dapper;
using InventoryVenturus.Data.Interfaces;
using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using System.Data;

namespace InventoryVenturus.Repositories
{
    public class ErrorLogRepository(IDataContext dataContext) : IErrorLogRepository
    {
        private IDbConnection Connection => dataContext.CreateConnection();

        public async Task AddErrorLogAsync(ErrorLog log)
        {
            using IDbConnection dbConnection = Connection;
            string query = "INSERT INTO ErrorLogs (Id, CorrelationId, RequestType, Request, Exception, Timestamp) VALUES (@Id, @CorrelationId, @RequestType, @Request, @Exception, @Timestamp)";
            dbConnection.Open();
            await dbConnection.ExecuteAsync(query, log);
        }
    }
}
