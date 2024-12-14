using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;
}
