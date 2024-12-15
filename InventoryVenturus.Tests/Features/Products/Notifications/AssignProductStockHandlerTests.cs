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
    public class AssignProductStockHandlerTests
    {
        private readonly Mock<ILogger<AssignProductStockHandler>> _loggerMock;
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly AssignProductStockHandler _handler;

        public AssignProductStockHandlerTests()
        {
            _loggerMock = new Mock<ILogger<AssignProductStockHandler>>();
            _stockRepositoryMock = new Mock<IStockRepository>();
            _handler = new AssignProductStockHandler(_loggerMock.Object, _stockRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateStockRecord_WhenProductCreatedNotificationIsReceived()
        {
            // Arrange
            var notification = new ProductCreatedNotification(Guid.NewGuid());
            _stockRepositoryMock.Setup(repo => repo.AddStockAsync(It.IsAny<Domain.Stock>()))
                                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.AddStockAsync(It.Is<Domain.Stock>(s => s.ProductId == notification.Id && s.Quantity == 0)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenExceptionIsThrown()
        {
            // Arrange
            var notification = new ProductCreatedNotification(Guid.NewGuid());
            var exception = new Exception("Test exception");
            _stockRepositoryMock.Setup(repo => repo.AddStockAsync(It.IsAny<Domain.Stock>()))
                                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(notification, CancellationToken.None));
        }
    }
}
