using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Commands.Create;
using InventoryVenturus.Features.Products.Commands.Update;
using InventoryVenturus.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var command = UpdateProductCommandTestData.ValidCommand;
            _productRepositoryMock.Setup(repo => repo.UpdateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Id == command.Id && p.Name == command.Name && p.Partnumber == command.Partnumber)), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenUpdateFails()
        {
            // Arrange
            var command = UpdateProductCommandTestData.ValidCommand;
            _productRepositoryMock.Setup(repo => repo.UpdateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Id == command.Id && p.Name == command.Name && p.Partnumber == command.Partnumber)), Times.Once);
            Assert.False(result);
        }
    }
}
