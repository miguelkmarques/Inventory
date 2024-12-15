using InventoryVenturus.Features.Stock.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Notifications
{
    public class StockConsumedNotificationValidatorTests
    {
        private readonly StockConsumedNotificationValidator _validator;

        public StockConsumedNotificationValidatorTests()
        {
            _validator = new StockConsumedNotificationValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenNotificationIsValid()
        {
            // Arrange
            var notification = new StockConsumedNotification(Guid.NewGuid(), 10, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var notification = new StockConsumedNotification(Guid.Empty, 10, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenConsumedQuantityIsInvalid()
        {
            // Arrange
            var notification = new StockConsumedNotification(Guid.NewGuid(), -1, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ConsumedQuantity", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenUnitCostIsInvalid()
        {
            // Arrange
            var notification = new StockConsumedNotification(Guid.NewGuid(), 10, -1);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Price", result.Errors.Single().PropertyName);
        }
    }
}
