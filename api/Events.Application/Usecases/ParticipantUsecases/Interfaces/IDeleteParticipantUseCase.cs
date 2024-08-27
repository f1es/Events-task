namespace Events.Application.Usecases.ParticipantUsecases.Interfaces;

public interface IDeleteParticipantUseCase
{
    Task DeleteParticipantAsync(Guid eventId, Guid id, bool trackChanges);
}
