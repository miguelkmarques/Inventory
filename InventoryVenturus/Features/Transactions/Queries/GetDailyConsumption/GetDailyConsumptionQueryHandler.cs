using InventoryVenturus.Features.Transactions.Dtos;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption
{
    public class GetDailyConsumptionQueryHandler(ITransactionRepository transactionRepository) : IRequestHandler<GetDailyConsumptionQuery, List<DailyConsumptionDto>>
    {
        public async Task<List<DailyConsumptionDto>> Handle(GetDailyConsumptionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await transactionRepository.GetTransactionsByDateAsync(request.Date);

            var dailyTransactions = transactions
                .GroupBy(t => t.ProductId)
                .Select(g => new DailyConsumptionDto(
                    g.Key,
                    g.Sum(t => t.Quantity),
                    g.Sum(t => t.Quantity * t.Cost),
                    Math.Round(g.Sum(t => t.Quantity * t.Cost) / g.Sum(t => t.Quantity), 2)))
                .ToList();

            return dailyTransactions;
        }
    }
}
