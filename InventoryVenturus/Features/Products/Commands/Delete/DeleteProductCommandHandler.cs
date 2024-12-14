using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Commands.Update;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Delete
{

    public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            return await productRepository.DeleteProductAsync(command.Id);
        }
    }
}
