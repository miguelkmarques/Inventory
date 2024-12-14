using InventoryVenturus.Controllers;
using InventoryVenturus.Features.Products.Commands.Create;
using InventoryVenturus.Features.Products.Commands.Delete;
using InventoryVenturus.Features.Products.Commands.Update;
using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Features.Products.Queries.Get;
using InventoryVenturus.Features.Products.Queries.List;
using InventoryVenturus.Tests.Features.Products.Commands.Create;
using InventoryVenturus.Tests.Features.Products.Commands.Update;
using InventoryVenturus.Tests.Features.Products.TestData;
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
            var command = CreateProductCommandTestData.ValidCommand;
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
        public async Task GetProductById_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var productDto = ProductTestData.GetSampleProductDto;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(productDto);

            // Act
            var result = await _controller.GetProductById(productDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(productDto, okResult.Value);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFoundResult_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((ProductDto?)null);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ListProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = ProductTestData.GetSampleProductDtos;
            _mediatorMock.Setup(m => m.Send(It.IsAny<ListProductsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(products);

            // Act
            var result = await _controller.ListProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task ListProducts_ReturnsOkResult_WithEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var products = new List<ProductDto>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ListProductsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(products);

            // Act
            var result = await _controller.ListProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var command = UpdateProductCommandTestData.ValidCommand;
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateProduct(command.Id, command);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var command = UpdateProductCommandTestData.ValidCommand;
            var differentId = Guid.NewGuid();

            // Act
            var result = await _controller.UpdateProduct(differentId, command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var command = UpdateProductCommandTestData.ValidCommand;
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateProduct(command.Id, command);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenDeleteFails()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}
