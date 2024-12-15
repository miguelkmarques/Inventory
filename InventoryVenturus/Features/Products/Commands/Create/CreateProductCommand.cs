using FluentValidation;
using InventoryVenturus.Features.Common;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Create
{
    public record CreateProductCommand(string Partnumber, string Name) : IRequest<Guid>;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .ValidateName();

            RuleFor(x => x.Partnumber)
                .ValidatePartnumber();
        }
    }
}
