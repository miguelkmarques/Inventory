using InventoryVenturus.Features.Stock.Queries.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Queries.Get
{
    public class GetStockQueryValidatorTests
    {
        private readonly GetStockQueryValidator _validator;

        public GetStockQueryValidatorTests()
        {
            _validator = new GetStockQueryValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenQueryIsValid()
        {
            // Arrange
            var query = new GetStockQuery(Guid.NewGuid());

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            // Arrange
            var query = new GetStockQuery(Guid.Empty);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ProductId", result.Errors.Single().PropertyName);
        }
    }
}
