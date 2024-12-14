using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public class AssignProductStockHandler(ILogger<AssignProductStockHandler> logger, IStockRepository stockRepository) : INotificationHandler<ProductCreatedNotification>
    {
        public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
