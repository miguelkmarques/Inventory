using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public record ProductDeletionRequestedNotification(Guid Id) : INotification;
}
