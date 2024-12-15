using InventoryVenturus.Repositories;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public class DeleteProductTransactionsHandler(ILogger<DeleteProductTransactionsHandler> logger, ITransactionRepository transactionRepository) : INotificationHandler<ProductDeletionRequestedNotification>
    {
        public async Task Handle(ProductDeletionRequestedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await transactionRepository.DeleteProductTransactionsAsync(notification.Id);
                if (deletedCount > 0)
                {
                    logger.LogInformation("{deletedCount} Transactions deleted for product with ID: {Id}", deletedCount, notification.Id);
                }
                else
                {
                    logger.LogWarning("No transactions found for product with ID: {Id}", notification.Id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting transactions for product with ID: {Id}", notification.Id);
                throw;
            }
        }
    }
}
