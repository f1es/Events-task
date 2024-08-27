namespace Events.Application.Usecases.EventUsecases.Interfaces;

public interface IDeleteEventUseCase
{
    Task DeleteEventAsync(Guid id, bool trackChanges);
}
