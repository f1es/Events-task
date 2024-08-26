namespace Events.Application.Usecases.Event.Interfaces;

public interface IDeleteEventUseCase
{
	Task DeleteEventAsync(Guid id, bool trackChanges);
}
