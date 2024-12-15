using FluentValidation.Results;
using FluentValidation;
using InventoryVenturus.Behaviors;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Behaviors
{
    public class ValidationBehaviorTests
    {
        private readonly Mock<IValidator<TestRequest>> _validatorMock;
        private readonly ValidationBehavior<TestRequest, TestResponse> _behavior;

        public ValidationBehaviorTests()
        {
            _validatorMock = new Mock<IValidator<TestRequest>>();
            var validators = new List<IValidator<TestRequest>> { _validatorMock.Object };
            _behavior = new ValidationBehavior<TestRequest, TestResponse>(validators);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var request = new TestRequest();
            var cancellationToken = new CancellationToken();
            var failures = new List<ValidationFailure>
            {
                new("Property1", "Error message 1"),
                new("Property2", "Error message 2")
            };
            _validatorMock.Setup(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()))
                          .Returns(new ValidationResult(failures));
            RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(new TestResponse());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _behavior.Handle(request, next, cancellationToken));
            Assert.Equal(failures, ex.Errors);
        }

        [Fact]
        public async Task Handle_ShouldCallNext_WhenValidationSucceeds()
        {
            // Arrange
            var request = new TestRequest();
            var response = new TestResponse();
            var cancellationToken = new CancellationToken();
            _validatorMock.Setup(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()))
                          .Returns(new ValidationResult());
            RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

            // Act
            var result = await _behavior.Handle(request, next, cancellationToken);

            // Assert
            Assert.Equal(response, result);
        }

        public class TestRequest : IRequest<TestResponse> { }
        public class TestResponse { }
    }
}
