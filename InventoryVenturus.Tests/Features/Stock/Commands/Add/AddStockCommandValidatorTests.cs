using InventoryVenturus.Features.Stock.Commands.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Commands.Add
{
    public class AddStockCommandValidatorTests
    {
        private readonly AddStockCommandValidator _validator;

        public AddStockCommandValidatorTests()
        {
            _validator = new AddStockCommandValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenCommandIsValid()
        {
            // Arrange
            var command = new AddStockCommand(Guid.NewGuid(), 10, 10.5m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var command = new AddStockCommand(Guid.Empty, 10, 10.5m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAddedQuantityIsInvalid()
        {
            // Arrange
            var command = new AddStockCommand(Guid.NewGuid(), -1, 10.5m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Quantity", result.Errors.Single().PropertyName);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAddedPriceIsInvalid()
        {
            // Arrange
            var command = new AddStockCommand(Guid.NewGuid(), 10, -1);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("UnitPrice", result.Errors.Single().PropertyName);
        }
    }
}
