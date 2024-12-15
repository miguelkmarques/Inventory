using InventoryVenturus.Features.Transactions.Dtos;
using MediatR;

namespace InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption
{
    public record GetDailyConsumptionQuery(DateTime Date) : IRequest<List<DailyConsumptionDto>>;
}
