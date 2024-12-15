using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
