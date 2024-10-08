﻿using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.ParticipantUsecases.Interfaces;

public interface IUpdateParticipantUseCase
{
    Task UpdateParticipantAsync(
        Guid eventId,
        Guid id,
        ParticipantRequestDto participant,
        bool trackChanges);
}
