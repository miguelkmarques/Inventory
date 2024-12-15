using FluentValidation;
using InventoryVenturus.Features.Common;
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
                .ValidateId("ProductId");

            RuleFor(x => x.AddedQuantity)
                .ValidateQuantity("AddedQuantity");

            RuleFor(x => x.FinalQuantity)
                .ValidateQuantity("FinalQuantity");

            RuleFor(x => x.UnitPrice)
                .ValidatePrice("UnitPrice");
        }
    }


}
