using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class UserLoginRequestDtoValidation : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginRequestDtoValidation()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(u => u.Password)
            .NotEmpty()
            .MaximumLength(100);
    }
}
