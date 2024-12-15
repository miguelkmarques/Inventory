using InventoryVenturus.Features.Stock.Commands.Consume;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Commands.Consume
{
    public class ConsumeStockCommandHandlerTests
    {
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ConsumeStockCommandHandler _handler;

        public ConsumeStockCommandHandlerTests()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new ConsumeStockCommandHandler(_stockRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDecreaseStockAndReturnTrue()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            var existingStock = new Domain.Stock(command.ProductId, 10);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync(existingStock);
            _stockRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<Domain.Stock>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.GetStockByProductIdAsync(command.ProductId), Times.Once);
            _stockRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.Is<Domain.Stock>(s => s.ProductId == command.ProductId && s.Quantity == 5)), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenUpdateStockFails()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            var existingStock = new Domain.Stock(command.ProductId, 10);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync(existingStock);
            _stockRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<Domain.Stock>())).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenStockRepositoryFails()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ThrowsAsync(new Exception("Repository failure"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
