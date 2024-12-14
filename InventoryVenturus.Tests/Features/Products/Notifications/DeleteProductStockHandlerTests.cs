using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Notifications
{
    public class DeleteProductStockHandlerTests
    {
        private readonly Mock<ILogger<DeleteProductStockHandler>> _loggerMock;
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly DeleteProductStockHandler _handler;

        public DeleteProductStockHandlerTests()
        {
            _loggerMock = new Mock<ILogger<DeleteProductStockHandler>>();
            _stockRepositoryMock = new Mock<IStockRepository>();
            _handler = new DeleteProductStockHandler(_loggerMock.Object, _stockRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteStockRecord_WhenProductDeletionRequestedNotificationIsReceived()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.NewGuid());
            var stock = new Domain.Stock(Guid.NewGuid(), 0)
            {
                ProductId = notification.Id
            };
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(notification.Id)).ReturnsAsync(stock);
            _stockRepositoryMock.Setup(repo => repo.DeleteStockAsync(stock.Id)).ReturnsAsync(true);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.GetStockByProductIdAsync(notification.Id), Times.Once);
            _stockRepositoryMock.Verify(repo => repo.DeleteStockAsync(stock.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotDeleteStockRecord_WhenStockNotFound()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.NewGuid());
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(notification.Id)).ReturnsAsync((Domain.Stock?)null);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.GetStockByProductIdAsync(notification.Id), Times.Once);
            _stockRepositoryMock.Verify(repo => repo.DeleteStockAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
