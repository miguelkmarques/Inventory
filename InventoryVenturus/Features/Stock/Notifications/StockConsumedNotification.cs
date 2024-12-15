using FluentValidation;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public record StockConsumedNotification(Guid ProductId, int ConsumedQuantity, decimal Price) : INotification;

    public class StockConsumedNotificationValidator : AbstractValidator<StockConsumedNotification>
    {
        public StockConsumedNotificationValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.ConsumedQuantity)
                .NotEmpty().WithMessage("ConsumedQuantity is required.")
                .GreaterThan(0).WithMessage("ConsumedQuantity must be greater than 0.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .PrecisionScale(18, 2, true).WithMessage("Price must have no more than 2 decimal places.");
        }
    }
}
