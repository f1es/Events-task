namespace Events.Application.Usecases.Image.Interfaces;

public interface IDeleteImageUseCase
{
	Task DeleteImageAsync(Guid eventId, bool trackChanges);
}
