using FluentValidation;
using InventoryVenturus.Features.Common;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public record StockConsumedNotification(Guid ProductId, int ConsumedQuantity, decimal Price) : INotification;

    public class StockConsumedNotificationValidator : AbstractValidator<StockConsumedNotification>
    {
        public StockConsumedNotificationValidator()
        {
            RuleFor(x => x.ProductId)
                .ValidateId("ProductId");

            RuleFor(x => x.ConsumedQuantity)
                .ValidateQuantity("ConsumedQuantity");

            RuleFor(x => x.Price)
                .ValidatePrice();
        }
    }
}
