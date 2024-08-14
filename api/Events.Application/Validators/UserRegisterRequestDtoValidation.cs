using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class UserRegisterRequestDtoValidation : AbstractValidator<UserRegisterRequestDto>
{
    public UserRegisterRequestDtoValidation()
    {
        RuleFor(u => u.Username)
            .Length(4, 50)
            .NotEmpty();

        RuleFor(u => u.Password)
            .MinimumLength(6).WithMessage("Password length must be at least 6 characters")
            .MaximumLength(50).WithMessage("Password length must be less than 50 characters")
            .NotEmpty();

        RuleFor(u => u.Email)
            .EmailAddress()
            .NotEmpty();
    }
}
