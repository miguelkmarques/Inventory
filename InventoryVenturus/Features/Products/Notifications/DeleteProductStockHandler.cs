using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public class DeleteProductStockHandler(ILogger<DeleteProductStockHandler> logger, IStockRepository stockRepository) : INotificationHandler<ProductDeletionRequestedNotification>
    {
        public async Task Handle(ProductDeletionRequestedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var result = await stockRepository.DeleteStockAsync(notification.Id);
                if (result)
                {
                    logger.LogInformation("Stock deleted for product with ID: {Id}", notification.Id);

                }
                else
                {
                    logger.LogInformation("Stock not found for product with ID: {Id}", notification.Id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting stock for product with ID: {Id}", notification.Id);
                throw;
            }
        }
    }
}
