using Events.Application.Usecases.EventUsecases.Interfaces;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class DeleteEventUseCase : IDeleteEventUseCase
{
    public Task DeleteEventAsync(Guid id, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}
