using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Create
{
    public record CreateProductCommand(string Partnumber, string Name) : IRequest<Guid>;
}
