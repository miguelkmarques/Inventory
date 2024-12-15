using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public class ConsumeTransactionHandler(ILogger<ConsumeTransactionHandler> logger, ITransactionRepository transactionRepository) : INotificationHandler<StockConsumedNotification>
    {
        public Task Handle(StockConsumedNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
