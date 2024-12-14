using InventoryVenturus.Features.Stock.Commands.Add;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Stock.Commands.Add
{
    public class AddStockCommandHandlerTests
    {
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AddStockCommandHandler _handler;

        public AddStockCommandHandlerTests()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new AddStockCommandHandler(_stockRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldIncreaseStockAndReturnTrue()
        {
            // Arrange
            var command = new AddStockCommand(Guid.NewGuid(), 10, 100m);
            var existingStock = new Domain.Stock(command.ProductId, 5);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync(existingStock);
            _stockRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<Domain.Stock>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.GetStockByProductIdAsync(command.ProductId), Times.Once);
            _stockRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.Is<Domain.Stock>(s => s.ProductId == command.ProductId && s.Quantity == 15)), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenUpdateStockFails()
        {
            // Arrange
            var command = new AddStockCommand(Guid.NewGuid(), 10, 100m);
            var existingStock = new Domain.Stock(command.ProductId, 5);
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
            var command = new AddStockCommand(Guid.NewGuid(), 10, 100m);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ThrowsAsync(new Exception("Repository failure"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
