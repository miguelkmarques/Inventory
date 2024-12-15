using InventoryVenturus.Controllers;
using InventoryVenturus.Features.Transactions.Dtos;
using InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption;
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
    public class TransactionsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TransactionsController _controller;

        public TransactionsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TransactionsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetDailyConsumption_ReturnsOkResult_WithDailyConsumption()
        {
            // Arrange
            var date = DateTime.Today;
            var dailyConsumption = new List<DailyConsumptionDto>
            {
                new(Guid.NewGuid(), 10, 100)
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDailyConsumptionQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(dailyConsumption);

            // Act
            var result = await _controller.GetDailyConsumption(date);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<DailyConsumptionDto>>(okResult.Value);
            Assert.Equal(dailyConsumption, returnValue);
        }

        [Fact]
        public async Task GetDailyConsumption_ReturnsOkResult_WithEmptyList()
        {
            // Arrange
            var date = DateTime.Today;
            var dailyConsumption = new List<DailyConsumptionDto>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDailyConsumptionQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(dailyConsumption);

            // Act
            var result = await _controller.GetDailyConsumption(date);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<DailyConsumptionDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetDailyConsumption_UsesTodaysDate_WhenDateNotSpecified()
        {
            // Arrange
            var dailyConsumption = new List<DailyConsumptionDto>
            {
                new(Guid.NewGuid(), 10, 100.0m)
            };
            _mediatorMock.Setup(m => m.Send(It.Is<GetDailyConsumptionQuery>(q => q.Date == DateTime.UtcNow.Date), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(dailyConsumption);

            // Act
            var result = await _controller.GetDailyConsumption(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<DailyConsumptionDto>>(okResult.Value);
            Assert.Equal(dailyConsumption, returnValue);
        }
    }
}
