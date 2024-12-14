using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public class AddTransactionHandler(ILogger<AddTransactionHandler> logger, ITransactionRepository transactionRepository) : INotificationHandler<StockAddedNotification>
    {
        public async Task Handle(StockAddedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = new Transaction(notification.ProductId, notification.AddedQuantity, TransactionType.Addition, DateTime.UtcNow, notification.Price);

                await transactionRepository.AddTransactionAsync(transaction);

                logger.LogInformation("Transaction added for product with ID: {Id}", notification.ProductId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding transaction for product with ID: {Id}", notification.ProductId);
                throw;
            }
        }
    }
}
