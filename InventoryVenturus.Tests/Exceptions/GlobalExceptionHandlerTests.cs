using FluentValidation;
using FluentValidation.Results;
using InventoryVenturus.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Exceptions
{
    public class GlobalExceptionHandlerTests
    {
        private readonly Mock<ILogger<GlobalExceptionHandler>> _loggerMock;
        private readonly GlobalExceptionHandler _handler;

        public GlobalExceptionHandlerTests()
        {
            _loggerMock = new Mock<ILogger<GlobalExceptionHandler>>();
            _handler = new GlobalExceptionHandler(_loggerMock.Object);
        }

        [Fact]
        public async Task TryHandleAsync_ShouldHandleValidationException()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var validationException = new ValidationException("Validation failed",
            [
                new ValidationFailure("Property1", "Error1"),
                new ValidationFailure("Property2", "Error2")
            ]);

            // Act
            var result = await _handler.TryHandleAsync(httpContext, validationException, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task TryHandleAsync_ShouldHandleBaseException()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var baseException = new BaseException("Base exception occurred", System.Net.HttpStatusCode.InternalServerError);

            // Act
            var result = await _handler.TryHandleAsync(httpContext, baseException, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task TryHandleAsync_ShouldHandleGeneralException()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var generalException = new Exception("General exception occurred");

            // Act
            var result = await _handler.TryHandleAsync(httpContext, generalException, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
        }

    }
}
