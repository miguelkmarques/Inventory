using InventoryVenturus.Exceptions;
using InventoryVenturus.Features.Stock.Commands.Consume;
using InventoryVenturus.Features.Stock.Notifications;
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
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ConsumeStockCommandHandler _handler;

        public ConsumeStockCommandHandlerTests()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new ConsumeStockCommandHandler(_stockRepositoryMock.Object, _productRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDecreaseStockAndReturnTrue()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            var existingStock = new Domain.Stock(command.ProductId, 10);
            var existingProduct = new Domain.Product { Id = command.ProductId, Price = 50 };
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync(existingStock);
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(command.ProductId)).ReturnsAsync(existingProduct);
            _stockRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<Domain.Stock>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.GetStockByProductIdAsync(command.ProductId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.GetProductByIdAsync(command.ProductId), Times.Once);
            _stockRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.Is<Domain.Stock>(s => s.ProductId == command.ProductId && s.Quantity == 5)), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.Is<StockConsumedNotification>(n => n.ProductId == command.ProductId && n.ConsumedQuantity == command.Quantity && n.Price == existingProduct.Price), It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenStockOrProductNotFound()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync((Domain.Stock)null);
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(command.ProductId)).ReturnsAsync((Domain.Product)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInsufficientStock()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 15);
            var existingStock = new Domain.Stock(command.ProductId, 10);
            var existingProduct = new Domain.Product { Id = command.ProductId, Price = 50 };
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync(existingStock);
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(command.ProductId)).ReturnsAsync(existingProduct);

            // Act & Assert
            await Assert.ThrowsAsync<InsufficientStockException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenUpdateStockFails()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            var existingStock = new Domain.Stock(command.ProductId, 10);
            var existingProduct = new Domain.Product { Id = command.ProductId, Price = 50 };
            _stockRepositoryMock.Setup(repo => repo.GetStockByProductIdAsync(command.ProductId)).ReturnsAsync(existingStock);
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(command.ProductId)).ReturnsAsync(existingProduct);
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
