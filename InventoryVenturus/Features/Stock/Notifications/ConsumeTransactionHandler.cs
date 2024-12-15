using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public class ConsumeTransactionHandler(ILogger<ConsumeTransactionHandler> logger, ITransactionRepository transactionRepository) : INotificationHandler<StockConsumedNotification>
    {
        public async Task Handle(StockConsumedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = new Transaction(notification.ProductId, notification.ConsumedQuantity, TransactionType.Consumption, DateTime.UtcNow, notification.Price);

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
