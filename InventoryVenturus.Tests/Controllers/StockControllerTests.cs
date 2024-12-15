using InventoryVenturus.Controllers;
using InventoryVenturus.Exceptions;
using InventoryVenturus.Features.Stock.Commands.Add;
using InventoryVenturus.Features.Stock.Commands.Consume;
using InventoryVenturus.Features.Stock.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Controllers
{
    public class StockControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly StockController _controller;

        public StockControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new StockController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetStock_ReturnsOkResult_WhenStockExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var stockQuantity = 10;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetStockQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(stockQuantity);

            // Act
            var result = await _controller.GetStock(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(stockQuantity, okResult.Value);
        }

        [Fact]
        public async Task GetStock_ThrowsException_WhenStockDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetStockQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((int?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => _controller.GetStock(productId));
        }

        [Fact]
        public async Task AddStock_ReturnsNoContent_WhenAddIsSuccessful()
        {
            // Arrange
            var command = new AddStockCommand(Guid.NewGuid(), 10, 120);
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddStockCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.AddStock(command);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task ConsumeStock_ReturnsNoContent_WhenConsumeIsSuccessful()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            _mediatorMock.Setup(m => m.Send(It.IsAny<ConsumeStockCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.ConsumeStock(command);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task ConsumeStock_ThrowsException_WhenConsumeFails()
        {
            // Arrange
            var command = new ConsumeStockCommand(Guid.NewGuid(), 5);
            _mediatorMock.Setup(m => m.Send(It.IsAny<ConsumeStockCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new InsufficientStockException(command.ProductId, command.Quantity, 0));

            // Act & Assert
            await Assert.ThrowsAsync<InsufficientStockException>(() => _controller.ConsumeStock(command));
        }
    }
}
