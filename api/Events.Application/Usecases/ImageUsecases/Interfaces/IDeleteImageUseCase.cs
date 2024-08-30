namespace Events.Application.Usecases.ImageUsecases.Interfaces;

public interface IDeleteImageUseCase
{
    Task DeleteImageAsync(Guid eventId, bool trackChanges);
}
