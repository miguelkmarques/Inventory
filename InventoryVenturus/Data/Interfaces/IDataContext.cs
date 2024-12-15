using System.Data;

namespace InventoryVenturus.Data.Interfaces
{
    public interface IDataContext
    {
        IDbConnection CreateConnection();

        Task InitDatabase();
    }
}

