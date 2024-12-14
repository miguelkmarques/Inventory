using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Commands.Create;
using InventoryVenturus.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Create
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddProductAndReturnProductId()
        {
            // Arrange
            var command = CreateProductCommandTestData.ValidCommand;
            var productId = Guid.NewGuid();
            _productRepositoryMock.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
                .Callback<Product>(product => product.Id = productId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.AddProductAsync(It.Is<Product>(p => p.Name == command.Name && p.Partnumber == command.Partnumber)), Times.Once);
            Assert.Equal(productId, result);
        }
    }
}
