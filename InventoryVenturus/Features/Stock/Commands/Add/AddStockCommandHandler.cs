using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Features.Stock.Notifications;
using InventoryVenturus.Repositories;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System.Transactions;

namespace InventoryVenturus.Features.Stock.Commands.Add
{
    public class AddStockCommandHandler(IStockRepository stockRepository, IMediator mediator) : IRequestHandler<AddStockCommand, bool>
    {
        public async Task<bool> Handle(AddStockCommand command, CancellationToken cancellationToken)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var existingStock = await stockRepository.GetStockByProductIdAsync(command.ProductId);
                if (existingStock is null)
                {
                    return false;
                }
                existingStock.Quantity += command.Quantity;
                var result = await stockRepository.UpdateStockAsync(existingStock);
                if (result)
                {
                    await mediator.Publish(new StockAddedNotification(existingStock.ProductId, command.Quantity, existingStock.Quantity, command.Price), cancellationToken);
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
