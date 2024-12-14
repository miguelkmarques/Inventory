using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System;

namespace InventoryVenturus.Features.Products.Commands
{
    public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product(command.Name, command.Partnumber);
            await productRepository.AddProductAsync(product);
            return product.Id;
        }
    }
}
