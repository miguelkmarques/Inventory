using FluentValidation;
using InventoryVenturus.Features.Common;
using MediatR;

namespace InventoryVenturus.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, string Partnumber, string Name) : IRequest<bool>;

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .ValidateId();

            RuleFor(x => x.Name)
                .ValidateName();

            RuleFor(x => x.Partnumber)
                .ValidatePartnumber();
        }
    }
}
