using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public record ProductDeletionRequestedNotification(Guid Id) : INotification;

    public class ProductDeletionRequestedNotificationValidator : AbstractValidator<ProductDeletionRequestedNotification>
    {
        public ProductDeletionRequestedNotificationValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
