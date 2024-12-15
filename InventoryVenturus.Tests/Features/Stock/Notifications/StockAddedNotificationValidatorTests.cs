using InventoryVenturus.Features.Stock.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Notifications
{
    public class StockAddedNotificationValidatorTests
    {
        private readonly StockAddedNotificationValidator _validator;

        public StockAddedNotificationValidatorTests()
        {
            _validator = new StockAddedNotificationValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenNotificationIsValid()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, 20, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.Empty, 10, 20, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAddedQuantityIsInvalid()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), -1, 20, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("AddedQuantity", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenFinalQuantityIsInvalid()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, -1, 10.5m);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("FinalQuantity", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenUnitPriceIsInvalid()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, 20, -1);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("UnitPrice", result.Errors.Single().PropertyName);
        }
    }
}
