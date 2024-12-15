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
    public class ConsumeTransactionHandlerTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<ILogger<ConsumeTransactionHandler>> _loggerMock;
        private readonly ConsumeTransactionHandler _handler;

        public ConsumeTransactionHandlerTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _loggerMock = new Mock<ILogger<ConsumeTransactionHandler>>();
            _handler = new ConsumeTransactionHandler(_loggerMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddTransaction_WhenStockConsumedNotificationIsReceived()
        {
            // Arrange
            var notification = new StockConsumedNotification(Guid.NewGuid(), 5, 50);
            _transactionRepositoryMock.Setup(repo => repo.AddTransactionAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _transactionRepositoryMock.Verify(repo => repo.AddTransactionAsync(It.Is<Transaction>(t =>
                t.ProductId == notification.ProductId &&
                t.Quantity == notification.ConsumedQuantity &&
                t.TransactionType == TransactionType.Consumption &&
                t.Cost == notification.Price
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenExceptionIsThrown()
        {
            // Arrange
            var notification = new StockConsumedNotification(Guid.NewGuid(), 5, 50);
            _transactionRepositoryMock.Setup(repo => repo.AddTransactionAsync(It.IsAny<Transaction>())).ThrowsAsync(new Exception("Repository failure"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(notification, CancellationToken.None));
            _transactionRepositoryMock.Verify(repo => repo.AddTransactionAsync(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
