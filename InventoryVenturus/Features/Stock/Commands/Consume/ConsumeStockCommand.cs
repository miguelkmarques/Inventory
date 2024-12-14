using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Consume
{
    public record ConsumeStockCommand(Guid ProductId, int Quantity) : IRequest<bool>;

}
