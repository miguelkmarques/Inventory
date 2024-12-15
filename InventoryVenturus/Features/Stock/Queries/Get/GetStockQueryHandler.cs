using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Queries.Get
{
    public class GetStockQueryHandler(IStockRepository stockRepository) : IRequestHandler<GetStockQuery, int?>
    {
        public Task<int?> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
