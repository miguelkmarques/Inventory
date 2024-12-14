
using InventoryVenturus.Domain;

namespace InventoryVenturus.Repositories.Interfaces
{
    public interface IErrorLogRepository
    {
        Task AddErrorLogAsync(ErrorLog log);
    }
}
