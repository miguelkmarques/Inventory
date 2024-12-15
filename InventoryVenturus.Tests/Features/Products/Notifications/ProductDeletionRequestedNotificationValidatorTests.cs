using InventoryVenturus.Features.Products.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Notifications
{
    public class ProductDeletionRequestedNotificationValidatorTests
    {
        private readonly ProductDeletionRequestedNotificationValidator _validator;

        public ProductDeletionRequestedNotificationValidatorTests()
        {
            _validator = new ProductDeletionRequestedNotificationValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenNotificationIsValid()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.NewGuid());

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.Empty);

            // Act
            var result = _validator.Validate(notification);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Id", result.Errors.Single().PropertyName);
        }
    }
}
