using FluentValidation;
using InventoryVenturus.Features.Products.Queries.Get;
using MediatR;

namespace InventoryVenturus.Features.Stock.Queries.Get
{
    public record GetStockQuery(Guid ProductId) : IRequest<int?>;

    public class GetStockQueryValidator : AbstractValidator<GetStockQuery>
    {
        public GetStockQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
        }
    }

}
