using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, string Partnumber, string Name) : IRequest<bool>;

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.Partnumber)
                .NotEmpty().WithMessage("Partnumber is required.")
                .MaximumLength(50).WithMessage("Partnumber must not exceed 255 characters.");
        }
    }
}
