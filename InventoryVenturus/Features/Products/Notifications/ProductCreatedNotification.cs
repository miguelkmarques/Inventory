﻿using FluentValidation;
using InventoryVenturus.Features.Common;
using InventoryVenturus.Features.Products.Commands.Update;
using MediatR;

namespace InventoryVenturus.Features.Products.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;

    public class ProductCreatedNotificationValidator : AbstractValidator<ProductCreatedNotification>
    {
        public ProductCreatedNotificationValidator()
        {
            RuleFor(x => x.Id)
                .ValidateId();
        }
    }
}
