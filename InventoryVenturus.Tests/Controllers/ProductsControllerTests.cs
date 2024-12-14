using InventoryVenturus.Controllers;
using InventoryVenturus.Features.Products.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var command = new CreateProductCommand("Part123", "ProductName");
            var productId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(productId);

            // Act
            var result = await _controller.CreateProduct(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetProductById), createdAtActionResult.ActionName);
            Assert.Equal(productId, createdAtActionResult.RouteValues?["id"]);
            Assert.Equal(productId, createdAtActionResult.Value);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
