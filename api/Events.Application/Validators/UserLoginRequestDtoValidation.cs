using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class UserLoginRequestDtoValidation : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginRequestDtoValidation()
    {
        RuleFor(u => u.Username)
            .Length(4, 50).WithMessage("Incorrect username")
            .NotEmpty();

        RuleFor(u => u.Password)
            .Length(6, 50).WithMessage("Incorrect password")
            .NotEmpty();
    }
}
