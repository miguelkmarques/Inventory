using MediatR;

namespace InventoryVenturus.Features.Stock.Queries.Get
{
    public class GetStockQueryHandler : IRequestHandler<GetStockQuery, int?>
    {
        public Task<int?> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
