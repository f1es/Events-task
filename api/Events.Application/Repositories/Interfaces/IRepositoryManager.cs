namespace Events.Application.Repositories.Interfaces;

public interface IRepositoryManager
{
	IParticipantRepository Participant { get; }
	IEventRepository Event { get; }
	IUserRepository User { get; }
	IImageRepository Image { get; }
	Task SaveAsync();
}
