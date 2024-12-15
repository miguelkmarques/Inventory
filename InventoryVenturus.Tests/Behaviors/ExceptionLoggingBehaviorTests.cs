using InventoryVenturus.Behaviors;
using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using Moq;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Behaviors
{
    public class ExceptionLoggingBehaviorTests
    {
        private readonly Mock<IErrorLogRepository> _errorLogRepositoryMock;
        private readonly ExceptionLoggingBehavior<TestRequest, TestResponse> _behavior;

        public ExceptionLoggingBehaviorTests()
        {
            _errorLogRepositoryMock = new Mock<IErrorLogRepository>();
            _behavior = new ExceptionLoggingBehavior<TestRequest, TestResponse>(_errorLogRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldLogException_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new TestRequest();
            var cancellationToken = new CancellationToken();
            var exception = new Exception("Test exception");
            RequestHandlerDelegate<TestResponse> next = () => throw exception;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _behavior.Handle(request, next, cancellationToken));
            Assert.Equal(exception, ex);

            _errorLogRepositoryMock.Verify(repo => repo.AddErrorLogAsync(It.Is<ErrorLog>(log => ValidateErrorLog(log, request, exception))), Times.Once);
        }

        private static bool ValidateErrorLog(ErrorLog log, TestRequest request, Exception exception)
        {
            return log.RequestType == typeof(TestRequest).Name &&
                log.Request == JsonSerializer.Serialize(request) &&
                (log.Exception?.Contains(exception.Message) ?? false) &&
                log.CorrelationId != Guid.Empty;
        }

        [Fact]
        public async Task Handle_ShouldReturnResponse_WhenNoExceptionIsThrown()
        {
            // Arrange
            var request = new TestRequest();
            var response = new TestResponse();
            var cancellationToken = new CancellationToken();
            RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

            // Act
            var result = await _behavior.Handle(request, next, cancellationToken);

            // Assert
            Assert.Equal(response, result);
            _errorLogRepositoryMock.Verify(repo => repo.AddErrorLogAsync(It.IsAny<ErrorLog>()), Times.Never);
        }
        public class TestRequest { }
        public class TestResponse { }
    }
}
