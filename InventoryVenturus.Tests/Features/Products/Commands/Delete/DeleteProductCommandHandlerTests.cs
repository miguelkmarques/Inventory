using InventoryVenturus.Features.Products.Commands.Delete;
using InventoryVenturus.Repositories.Interfaces;
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
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenDeleteIsSuccessful()
        {
            // Arrange
            var command = DeleteProductCommandTestData.ValidCommand;
            _productRepositoryMock.Setup(repo => repo.DeleteProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(command.Id), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenDeleteFails()
        {
            // Arrange
            var command = DeleteProductCommandTestData.ValidCommand;
            _productRepositoryMock.Setup(repo => repo.DeleteProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(command.Id), Times.Once);
            Assert.False(result);
        }
    }
}
