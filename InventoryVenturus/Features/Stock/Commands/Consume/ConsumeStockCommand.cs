using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Consume
{
    public record ConsumeStockCommand(Guid ProductId, int Quantity) : IRequest<bool>;

    public class ConsumeStockCommandValidator : AbstractValidator<ConsumeStockCommand>
    {
        public ConsumeStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }

}
