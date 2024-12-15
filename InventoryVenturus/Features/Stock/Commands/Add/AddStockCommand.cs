using FluentValidation;
using InventoryVenturus.Features.Common;
using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Add
{
    public record AddStockCommand(Guid ProductId, int Quantity, decimal UnitPrice) : IRequest<bool>;

    public class AddStockCommandValidator : AbstractValidator<AddStockCommand>
    {
        public AddStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .ValidateId("ProductId");

            RuleFor(x => x.Quantity)
                .ValidateQuantity();

            RuleFor(x => x.UnitPrice)
                .ValidatePrice("UnitPrice");
        }
    }

}
