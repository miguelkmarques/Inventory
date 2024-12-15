using FluentValidation;
using InventoryVenturus.Features.Common;
using InventoryVenturus.Features.Stock.Notifications;
using InventoryVenturus.Features.Transactions.Dtos;
using MediatR;

namespace InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption
{
    public record GetDailyConsumptionQuery(DateTime Date) : IRequest<List<DailyConsumptionDto>>;

    public class GetDailyConsumptionQueryValidator : AbstractValidator<GetDailyConsumptionQuery>
    {
        public GetDailyConsumptionQueryValidator()
        {
            RuleFor(x => x.Date)
                .ValidateQueryDate();
        }
    }
}
