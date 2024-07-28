namespace Events.Application.Repositories.Interfaces;

public interface IRepositoryManager
{
	IParticipantRepository Participant { get; }
	IEventRepository Event { get; }
	Task SaveAsync();
}
