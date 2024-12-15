using InventoryVenturus.Domain;
using InventoryVenturus.Exceptions;
using InventoryVenturus.Features.Stock.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Notifications
{
    public class UpdateProductAveragePriceHandlerTests
    {
        private readonly Mock<ILogger<UpdateProductAveragePriceHandler>> _loggerMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateProductAveragePriceHandler _handler;

        public UpdateProductAveragePriceHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateProductAveragePriceHandler>>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateProductAveragePriceHandler(_loggerMock.Object, _productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAveragePrice_WhenStockAddedNotificationIsReceived()
        {
            // Arrange
            var existingProduct = Products.TestData.ProductTestData.GetSampleProduct;
            var notification = new StockAddedNotification(existingProduct.Id, 10, 30, 120);
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(notification.ProductId)).ReturnsAsync(existingProduct);
            _productRepositoryMock.Setup(repo => repo.UpdateAveragePriceAsync(notification.ProductId, It.IsAny<decimal>())).ReturnsAsync(true);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProductByIdAsync(notification.ProductId), Times.Once);
            var expectedAveragePrice = ((existingProduct.Price * (notification.FinalQuantity - notification.AddedQuantity)) + (notification.Price * notification.AddedQuantity)) / notification.FinalQuantity;
            _productRepositoryMock.Verify(repo => repo.UpdateAveragePriceAsync(notification.ProductId, It.Is<decimal>(price => price == expectedAveragePrice)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotUpdateAveragePrice_WhenUpdateAveragePriceFails()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, 30, 120);
            var existingProduct = new Product("Test Product", "TP123") { Id = notification.ProductId, Price = 80m };
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(notification.ProductId)).ReturnsAsync(existingProduct);
            _productRepositoryMock.Setup(repo => repo.UpdateAveragePriceAsync(notification.ProductId, It.IsAny<decimal>())).ReturnsAsync(false);

            // Act
            await Assert.ThrowsAsync<ProductNotFoundException>(() => _handler.Handle(notification, CancellationToken.None));

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProductByIdAsync(notification.ProductId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenExceptionIsThrown()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, 30, 120);
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(notification.ProductId)).ThrowsAsync(new Exception("Repository failure"));

            // Act
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(notification, CancellationToken.None));

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProductByIdAsync(notification.ProductId), Times.Once);
        }
    }
}
