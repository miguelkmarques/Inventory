using FluentValidation;

namespace InventoryVenturus.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.Partnumber)
                .NotEmpty().WithMessage("Partnumber is required.")
                .MaximumLength(50).WithMessage("Partnumber must not exceed 255 characters.");
        }
    }
}
