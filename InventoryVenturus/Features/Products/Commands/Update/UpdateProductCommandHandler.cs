using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, bool>
    {
        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product(command.Name, command.Partnumber)
            {
                Id = command.Id
            };
            return await productRepository.UpdateProductAsync(product);
        }
    }
}
