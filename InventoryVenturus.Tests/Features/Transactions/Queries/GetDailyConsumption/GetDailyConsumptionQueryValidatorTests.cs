using FluentValidation.TestHelper;
using InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Transactions.Queries.GetDailyConsumption
{
    public class GetDailyConsumptionQueryValidatorTests
    {
        private readonly GetDailyConsumptionQueryValidator _validator;

        public GetDailyConsumptionQueryValidatorTests()
        {
            _validator = new GetDailyConsumptionQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Date_Is_In_The_Future()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(1);
            var query = new GetDailyConsumptionQuery(futureDate);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Date)
                .WithErrorMessage("Date cannot be in the future.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Date_Is_Today_Or_Past()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-1);
            var todayDate = DateTime.UtcNow.Date;
            var pastQuery = new GetDailyConsumptionQuery(pastDate);
            var todayQuery = new GetDailyConsumptionQuery(todayDate);

            // Act
            var pastResult = _validator.TestValidate(pastQuery);
            var todayResult = _validator.TestValidate(todayQuery);

            // Assert
            pastResult.ShouldNotHaveValidationErrorFor(x => x.Date);
            todayResult.ShouldNotHaveValidationErrorFor(x => x.Date);
        }
    }
}
