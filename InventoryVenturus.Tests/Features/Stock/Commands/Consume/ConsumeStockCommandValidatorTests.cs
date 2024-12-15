using InventoryVenturus.Features.Stock.Commands.Consume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Commands.Consume
{
    public class ConsumeStockCommandValidatorTests
    {
        private readonly ConsumeStockCommandValidator _validator;

        public ConsumeStockCommandValidatorTests()
        {
            _validator = new ConsumeStockCommandValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenCommandIsValid()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 10);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.Empty, 10);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenConsumedQuantityIsInvalid()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), -1);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Quantity", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenConsumedQuantityIsZero()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 0);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains(result.Errors, e => e.PropertyName == "Quantity");
        }
    }
}
