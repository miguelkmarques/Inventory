using FluentValidation.TestHelper;
using InventoryVenturus.Features.Products.Commands.Update;

namespace InventoryVenturus.Tests.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator _validator;

        public UpdateProductCommandValidatorTests()
        {
            _validator = new UpdateProductCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = UpdateProductCommandTestData.EmptyIdCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = UpdateProductCommandTestData.EmptyNameCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_MaxLength()
        {
            var command = UpdateProductCommandTestData.LongNameCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Partnumber_Is_Empty()
        {
            var command = UpdateProductCommandTestData.EmptyPartnumberCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Partnumber);
        }

        [Fact]
        public void Should_Have_Error_When_Partnumber_Exceeds_MaxLength()
        {
            var command = UpdateProductCommandTestData.LongPartnumberCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Partnumber);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = UpdateProductCommandTestData.ValidCommand;
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Partnumber);
        }
    }
}
