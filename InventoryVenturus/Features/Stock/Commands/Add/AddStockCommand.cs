using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Add
{
    public record AddStockCommand(Guid ProductId, int Quantity, decimal Price) : IRequest<bool>;

}
