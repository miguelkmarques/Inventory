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
    public class DeleteProductTransactionsHandlerTests
    {
        private readonly Mock<ILogger<DeleteProductTransactionsHandler>> _loggerMock;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly DeleteProductTransactionsHandler _handler;

        public DeleteProductTransactionsHandlerTests()
        {
            _loggerMock = new Mock<ILogger<DeleteProductTransactionsHandler>>();
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _handler = new DeleteProductTransactionsHandler(_loggerMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteTransactionRecord_WhenProductDeletionRequestedNotificationIsReceived()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.NewGuid());
            _transactionRepositoryMock.Setup(repo => repo.DeleteProductTransactionsAsync(notification.Id)).ReturnsAsync(2);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _transactionRepositoryMock.Verify(repo => repo.DeleteProductTransactionsAsync(notification.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotThrowException_WhenTransactionsNotFound()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.NewGuid());
            _transactionRepositoryMock.Setup(repo => repo.DeleteProductTransactionsAsync(notification.Id)).ReturnsAsync(0);

            // Act & Assert
            var exception = await Record.ExceptionAsync(() => _handler.Handle(notification, CancellationToken.None));
            Assert.Null(exception);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var notification = new ProductDeletionRequestedNotification(Guid.NewGuid());
            _transactionRepositoryMock.Setup(repo => repo.DeleteProductTransactionsAsync(notification.Id)).ThrowsAsync(new Exception("Repository exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(notification, CancellationToken.None));
        }
    }
}
