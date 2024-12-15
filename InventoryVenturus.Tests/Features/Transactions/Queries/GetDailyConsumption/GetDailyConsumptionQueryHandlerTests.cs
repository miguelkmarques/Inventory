using InventoryVenturus.Domain;
using InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption;
using InventoryVenturus.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Transactions.Queries.GetDailyConsumption
{
    public class GetDailyConsumptionQueryHandlerTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly GetDailyConsumptionQueryHandler _handler;

        public GetDailyConsumptionQueryHandlerTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _handler = new GetDailyConsumptionQueryHandler(_transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnGroupedDailyConsumption_WhenTransactionsExist()
        {
            // Arrange
            var date = new DateTime(2024, 12, 15);
            var productId = Guid.NewGuid();
            var transactions = new List<Transaction>
            {
                new(productId, 10, TransactionType.Consumption, date, 10),
                new(productId, 5, TransactionType.Consumption, date, 11),
                new(Guid.NewGuid(), 3, TransactionType.Consumption, date, 12)
            };

            _transactionRepositoryMock.Setup(repo => repo.GetAllTransactionsAsync())
                .ReturnsAsync(transactions);

            var query = new GetDailyConsumptionQuery(date);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var firstProduct = result.FirstOrDefault(r => r.ProductId == productId);
            Assert.NotNull(firstProduct);
            Assert.Equal(15, firstProduct.Quantity);
            Assert.Equal(155m, firstProduct.TotalPrice);
            Assert.Equal(10.33m, Math.Round(firstProduct.AverageUnitPrice, 2));

            var secondProduct = result.FirstOrDefault(r => r.ProductId != productId);
            Assert.NotNull(secondProduct);
            Assert.Equal(3, secondProduct.Quantity);
            Assert.Equal(12m, secondProduct.AverageUnitPrice);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoTransactionsExist()
        {
            // Arrange
            var date = new DateTime(2024, 12, 15);
            var transactions = new List<Transaction>();

            _transactionRepositoryMock.Setup(repo => repo.GetAllTransactionsAsync())
                .ReturnsAsync(transactions);

            var query = new GetDailyConsumptionQuery(date);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
