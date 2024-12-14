using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, string Partnumber, string Name) : IRequest<bool>;
}
