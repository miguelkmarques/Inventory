using Castle.Core.Logging;
using InventoryVenturus.Domain;
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
    public class AddTransactionHandlerTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<ILogger<AddTransactionHandler>> _loggerMock;
        private readonly AddTransactionHandler _handler;

        public AddTransactionHandlerTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _loggerMock = new Mock<ILogger<AddTransactionHandler>>();
            _handler = new AddTransactionHandler(_loggerMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddTransaction_WhenStockAddedNotificationIsReceived()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, 30, 100m);
            _transactionRepositoryMock.Setup(repo => repo.AddTransactionAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _transactionRepositoryMock.Verify(repo => repo.AddTransactionAsync(It.Is<Transaction>(t =>
                t.ProductId == notification.ProductId &&
                t.Quantity == notification.AddedQuantity &&
                t.TransactionType == TransactionType.Addition &&
                t.Cost == notification.Price
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var notification = new StockAddedNotification(Guid.NewGuid(), 10, 30, 100m);
            _transactionRepositoryMock.Setup(repo => repo.AddTransactionAsync(It.IsAny<Transaction>())).ThrowsAsync(new Exception("Repository failure"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(notification, CancellationToken.None));
            _transactionRepositoryMock.Verify(repo => repo.AddTransactionAsync(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
