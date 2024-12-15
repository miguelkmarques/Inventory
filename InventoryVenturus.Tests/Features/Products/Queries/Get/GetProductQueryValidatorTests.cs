using InventoryVenturus.Features.Products.Queries.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Queries.Get
{
    public class GetProductQueryValidatorTests
    {
        private readonly GetProductQueryValidator _validator;

        public GetProductQueryValidatorTests()
        {
            _validator = new GetProductQueryValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenQueryIsValid()
        {
            // Arrange
            var query = new GetProductQuery(Guid.NewGuid());

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var query = new GetProductQuery(Guid.Empty);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Id", result.Errors.Single().PropertyName);
        }
    }
}
