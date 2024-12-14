using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public record StockAddedNotification(Guid ProductId, int AddedQuantity, int FinalQuantity, decimal Price) : INotification;
}
