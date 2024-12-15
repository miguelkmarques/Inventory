using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Queries.Get
{
    public class GetStockQueryHandler(IStockRepository stockRepository) : IRequestHandler<GetStockQuery, int?>
    {
        public async Task<int?> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            var stock = await stockRepository.GetStockByProductIdAsync(request.ProductId);
            if (stock is null)
            {
                return null;
            }
            return stock.Quantity;
        }
    }
}
