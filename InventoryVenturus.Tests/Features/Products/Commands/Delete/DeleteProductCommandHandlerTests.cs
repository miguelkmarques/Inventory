using InventoryVenturus.Features.Products.Commands.Delete;
using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenProductIsDeletedSuccessfully()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());
            _productRepositoryMock.Setup(repo => repo.DeleteProductAsync(command.Id)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(command.Id), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<ProductDeletionRequestedNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDeletionFails()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());
            _productRepositoryMock.Setup(repo => repo.DeleteProductAsync(command.Id)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(command.Id), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<ProductDeletionRequestedNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAnErrorOccurs()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());
            _productRepositoryMock.Setup(repo => repo.DeleteProductAsync(command.Id)).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(command.Id), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<ProductDeletionRequestedNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
