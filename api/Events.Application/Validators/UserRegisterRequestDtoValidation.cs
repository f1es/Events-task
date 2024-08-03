using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class UserRegisterRequestDtoValidation : AbstractValidator<UserRegisterRequestDto>
{
    public UserRegisterRequestDtoValidation()
    {
        RuleFor(u => u.Username)
            .MinimumLength(4)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(u => u.Password)
            .MinimumLength(6)
            .MaximumLength(100)
            .NotEmpty();
    }
}
