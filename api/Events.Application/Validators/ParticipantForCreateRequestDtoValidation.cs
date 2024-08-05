using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class ParticipantForCreateRequestDtoValidation : AbstractValidator<ParticipantForCreateRequestDto>
{
    public ParticipantForCreateRequestDtoValidation()
    {
        RuleFor(p => p.Name)
            .MaximumLength(201)
            .NotEmpty();

        RuleFor(p => p.Surname)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(p => p.Birthday)
            .NotEmpty();

        RuleFor(p => p.Email)
            .NotEmpty();
    }
}
