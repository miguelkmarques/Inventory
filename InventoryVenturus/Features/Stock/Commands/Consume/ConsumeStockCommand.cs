using FluentValidation;
using InventoryVenturus.Features.Common;
using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Consume
{
    public record ConsumeStockCommand(Guid ProductId, int Quantity) : IRequest<bool>;

    public class ConsumeStockCommandValidator : AbstractValidator<ConsumeStockCommand>
    {
        public ConsumeStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .ValidateId("ProductId");

            RuleFor(x => x.Quantity)
                .ValidateQuantity();
        }
    }

}
