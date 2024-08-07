using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class EventForCreateRequestDtoValidation : AbstractValidator<EventForCreateRequestDto>
{
    public EventForCreateRequestDtoValidation()
    {
        RuleFor(e => e.Name)
            .MaximumLength(250)
            .NotEmpty();

        RuleFor(e => e.Description)
            .MaximumLength(500);

        RuleFor(e => e.Date)
            .NotEmpty();

        RuleFor(e => e.Place)
            .NotEmpty();

        RuleFor(e => e.Category)
            .MaximumLength(250);

        RuleFor(e => e.MaxParticipantsCount)
            .NotEmpty();
    }
}
