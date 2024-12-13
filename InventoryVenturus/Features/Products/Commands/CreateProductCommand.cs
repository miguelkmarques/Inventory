using MediatR;

namespace InventoryVenturus.Features.Products.Commands
{
    public record CreateProductCommand(string Partnumber, string Name) : IRequest<Guid>;
}
