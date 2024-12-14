using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Commands.Update;
using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System.Transactions;

namespace InventoryVenturus.Features.Products.Commands.Delete
{

    public class DeleteProductCommandHandler(IProductRepository productRepository, IMediator mediator) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {

            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await mediator.Publish(new ProductDeletionRequestedNotification(command.Id), cancellationToken);

                var result = await productRepository.DeleteProductAsync(command.Id);

                if (result)
                {
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
