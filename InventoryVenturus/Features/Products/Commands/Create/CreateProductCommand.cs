using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Create
{
    public record CreateProductCommand(string Partnumber, string Name) : IRequest<Guid>;

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
