using FluentValidation;
using InventoryVenturus.Features.Products.Notifications;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public record StockAddedNotification(Guid ProductId, int AddedQuantity, int FinalQuantity, decimal UnitPrice) : INotification;

    public class StockAddedNotificationValidator : AbstractValidator<StockAddedNotification>
    {
        public StockAddedNotificationValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.AddedQuantity)
                .NotEmpty().WithMessage("AddedQuantity is required.")
                .GreaterThan(0).WithMessage("AddedQuantity must be greater than 0.");

            RuleFor(x => x.FinalQuantity)
                .NotEmpty().WithMessage("FinalQuantity is required.")
                .GreaterThan(0).WithMessage("FinalQuantity must be greater than 0.");

            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .PrecisionScale(18, 2, true).WithMessage("UnitPrice must have no more than 2 decimal places.");
        }
    }


}
