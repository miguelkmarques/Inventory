using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Add
{
    public record AddStockCommand(Guid ProductId, int Quantity, decimal Price) : IRequest<bool>;

    public class AddStockCommandValidator : AbstractValidator<AddStockCommand>
    {
        public AddStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }

}
