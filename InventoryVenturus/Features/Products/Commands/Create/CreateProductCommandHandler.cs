using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System;
using System.Transactions;

namespace InventoryVenturus.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var product = new Product(command.Name, command.Partnumber);

                await productRepository.AddProductAsync(product);

                await mediator.Publish(new ProductCreatedNotification(product.Id), cancellationToken);

                transactionScope.Complete();

                return product.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
