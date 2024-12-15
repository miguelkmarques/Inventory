using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public class DeleteProductTransactionsHandler(ILogger<DeleteProductTransactionsHandler> logger, ITransactionRepository transactionRepository) : INotificationHandler<ProductDeletionRequestedNotification>
    {
        public Task Handle(ProductDeletionRequestedNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
