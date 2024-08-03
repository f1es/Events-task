﻿using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Validators;

public class ParticipantForUpdateRequestDtoValidation : AbstractValidator<ParticipantForUpdateRequestDto>
{
    public ParticipantForUpdateRequestDtoValidation()
    {
		RuleFor(p => p.Name)
			.NotEmpty()
			.MaximumLength(200);

		RuleFor(p => p.Surname)
			.NotEmpty()
			.MaximumLength(200);

		RuleFor(p => p.Birthday)
			.NotEmpty();

		RuleFor(p => p.Email)
			.NotEmpty();
	}
}
