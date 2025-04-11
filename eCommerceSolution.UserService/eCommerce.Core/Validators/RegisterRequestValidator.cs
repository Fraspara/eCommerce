using eCommerce.Core.Dto;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(temp => temp.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email address format");

        RuleFor(temp => temp.Password)
            .NotEmpty().WithMessage("Password is required");

        RuleFor(temp => temp.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(temp => temp.Gender)
            .NotEmpty()
            .IsInEnum();
    }
}
