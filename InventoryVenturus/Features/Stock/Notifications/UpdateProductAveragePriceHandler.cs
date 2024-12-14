using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public class UpdateProductAveragePriceHandler(ILogger<UpdateProductAveragePriceHandler> logger, IProductRepository productRepository) : INotificationHandler<StockAddedNotification>
    {
        public Task Handle(StockAddedNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
