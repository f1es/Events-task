namespace Events.Application.Usecases.Participant.Interfaces;

public interface IDeleteParticipantUseCase
{
	Task DeleteParticipantAsync(Guid eventId, Guid id, bool trackChanges);
}
