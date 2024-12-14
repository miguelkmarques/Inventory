using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public record StockConsumedNotification(Guid ProductId, int Quantity) : INotification;
}
