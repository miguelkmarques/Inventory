using FluentValidation;

namespace InventoryVenturus.Features.Common
{
    public static class ValidationUtils
    {
        public static IRuleBuilderOptions<T, Guid> ValidateId<T>(this IRuleBuilder<T, Guid> ruleBuilder, string name = "Id")
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{name} is required.");
        }

        public static IRuleBuilderOptions<T, string> ValidateName<T>(this IRuleBuilder<T, string> ruleBuilder, string name = "Name")
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{name} is required.")
                .MaximumLength(100).WithMessage($"{name} must not exceed 255 characters.");
        }

        public static IRuleBuilderOptions<T, string> ValidatePartnumber<T>(this IRuleBuilder<T, string> ruleBuilder, string name = "Partnumber")
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{name} is required.")
                .MaximumLength(50).WithMessage($"{name} must not exceed 255 characters.");
        }

        public static IRuleBuilderOptions<T, int> ValidateQuantity<T>(this IRuleBuilder<T, int> ruleBuilder, string name = "Quantity")
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{name} is required.")
                .GreaterThan(0).WithMessage($"{name} must be greater than 0.");
        }

        public static IRuleBuilderOptions<T, decimal> ValidatePrice<T>(this IRuleBuilder<T, decimal> ruleBuilder, string name = "Price")
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{name} is required.")
                .GreaterThan(0).WithMessage($"{name} must be greater than 0.")
                .PrecisionScale(18, 2, true).WithMessage($"{name} must have no more than 2 decimal places.");
        }
    }
}
