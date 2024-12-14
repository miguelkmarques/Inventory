using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Queries.Get;
using InventoryVenturus.Repositories.Interfaces;
using InventoryVenturus.Tests.Features.Products.TestData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Queries.Get
{
    public class GetProductQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductQueryHandler _handler;

        public GetProductQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange
            var product = ProductTestData.GetSampleProduct;
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(product.Id))
                .ReturnsAsync(product);

            var query = new GetProductQuery(product.Id);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Partnumber, result.Partnumber);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.AveragePrice);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var query = new GetProductQuery(productId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
