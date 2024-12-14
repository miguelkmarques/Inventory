using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}
