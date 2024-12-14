using FluentValidation.TestHelper;
using InventoryVenturus.Features.Products.Commands.Create;

namespace InventoryVenturus.Tests.Features.Products.Commands.Create
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = CreateProductCommandTestData.EmptyNameCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_MaxLength()
        {
            var command = CreateProductCommandTestData.LongNameCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Partnumber_Is_Empty()
        {
            var command = CreateProductCommandTestData.EmptyPartnumberCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Partnumber);
        }

        [Fact]
        public void Should_Have_Error_When_Partnumber_Exceeds_MaxLength()
        {
            var command = CreateProductCommandTestData.LongPartnumberCommand;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Partnumber);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = CreateProductCommandTestData.ValidCommand;
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Partnumber);
        }
    }
}
