using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public record StockAddedNotification(Guid ProductId, int Quantity, decimal Price) : INotification;
}
