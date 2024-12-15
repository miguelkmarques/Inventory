using InventoryVenturus.Features.Transactions.Dtos;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption
{
    public class GetDailyConsumptionQueryHandler(ITransactionRepository transactionRepository) : IRequestHandler<GetDailyConsumptionQuery, List<DailyConsumptionDto>>
    {
        public Task<List<DailyConsumptionDto>> Handle(GetDailyConsumptionQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
