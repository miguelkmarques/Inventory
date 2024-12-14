using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Queries.List;
using InventoryVenturus.Repositories.Interfaces;
using InventoryVenturus.Tests.Features.Products.TestData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Queries.List
{
    public class ListProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ListProductsQueryHandler _handler;

        public ListProductsQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new ListProductsQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfProductDtos_WhenProductsExist()
        {
            // Arrange
            var products = ProductTestData.GetSampleProducts;
            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(products);

            var query = new ListProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count, result.Count);
            for (int i = 0; i < products.Count; i++)
            {
                Assert.Equal(products[i].Id, result[i].Id);
                Assert.Equal(products[i].Partnumber, result[i].Partnumber);
                Assert.Equal(products[i].Name, result[i].Name);
                Assert.Equal(products[i].Price, result[i].AveragePrice);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync([]);

            var query = new ListProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
