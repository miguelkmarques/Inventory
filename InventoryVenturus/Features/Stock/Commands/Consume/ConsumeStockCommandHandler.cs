using InventoryVenturus.Exceptions;
using InventoryVenturus.Features.Stock.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System.Transactions;

namespace InventoryVenturus.Features.Stock.Commands.Consume
{
    public class ConsumeStockCommandHandler(IStockRepository stockRepository, IProductRepository productRepository, IMediator mediator) : IRequestHandler<ConsumeStockCommand, bool>
    {
        public async Task<bool> Handle(ConsumeStockCommand command, CancellationToken cancellationToken)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var existingStock = await stockRepository.GetStockByProductIdAsync(command.ProductId);
                var existingProduct = await productRepository.GetProductByIdAsync(command.ProductId);
                if (existingStock is null || existingProduct is null)
                {
                    return false;
                }
                var updatedQuantity = existingStock.Quantity - command.Quantity;
                if (updatedQuantity < 0)
                {
                    throw new InsufficientStockException(command.ProductId, command.Quantity, existingStock.Quantity);
                }
                existingStock.Quantity = updatedQuantity;
                var result = await stockRepository.UpdateStockAsync(existingStock);
                if (result)
                {
                    await mediator.Publish(new StockConsumedNotification(existingStock.ProductId, command.Quantity, existingProduct.Price), cancellationToken);
                    transactionScope.Complete();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
