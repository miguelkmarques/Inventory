using InventoryVenturus.Domain;
using InventoryVenturus.Repositories;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public class AssignProductStockHandler(ILogger<AssignProductStockHandler> logger, IStockRepository stockRepository) : INotificationHandler<ProductCreatedNotification>
    {
        public async Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var stock = new Stock(notification.Id, 0);

                await stockRepository.AddStockAsync(stock);

                logger.LogInformation("Stock assigned for product with ID: {Id}", notification.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error assigning stock for product with ID: {Id}", notification.Id);
                throw;
            }
        }
    }
}
