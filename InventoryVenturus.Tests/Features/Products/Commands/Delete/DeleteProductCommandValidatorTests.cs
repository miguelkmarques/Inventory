using InventoryVenturus.Features.Products.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Delete
{
    public class DeleteProductCommandValidatorTests
    {
        private readonly DeleteProductCommandValidator _validator;

        public DeleteProductCommandValidatorTests()
        {
            _validator = new DeleteProductCommandValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenCommandIsValid()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.Empty);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Id", result.Errors.Single().PropertyName);
        }
    }
}
