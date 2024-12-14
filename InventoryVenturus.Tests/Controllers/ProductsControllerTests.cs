using InventoryVenturus.Controllers;
using InventoryVenturus.Features.Products.Commands.Create;
using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Features.Products.Queries.Get;
using InventoryVenturus.Features.Products.Queries.List;
using InventoryVenturus.Tests.Features.Products.Commands.Create;
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
    }
}
