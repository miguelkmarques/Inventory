using InventoryVenturus.Features.Stock.Queries.Get;
using InventoryVenturus.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Queries.Get
{
    public class GetStockQueryHandlerTests
    {
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly GetStockQueryHandler _handler;

        public GetStockQueryHandlerTests()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _handler = new GetStockQueryHandler(_stockRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnStockCount_WhenStockExists()
        {
            // Arrange
            var stock = new Domain.Stock(Guid.NewGuid(), 10);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(stock.ProductId))
                .ReturnsAsync(stock);

            var query = new GetStockQuery(stock.ProductId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(stock.Quantity, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenStockDoesNotExist()
        {
            // Arrange
            var stock = new Domain.Stock(Guid.NewGuid(), 10);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(stock.ProductId))
                .ReturnsAsync((Domain.Stock?)null);

            var query = new GetStockQuery(stock.ProductId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
