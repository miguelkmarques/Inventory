using MediatR;

namespace InventoryVenturus.Features.Stock.Queries.Get
{
    public record GetStockQuery(Guid ProductId) : IRequest<int?>;

}
