using Events.Application.Repositories.Interfaces;
using Events.Infrastructure.Context;

namespace Events.Infrastructure.Repositories.Implementations;

public class RepositoryManager : IRepositoryManager
{
	private readonly EventsDBContext _eventsDBContext;
    private Lazy<IParticipantRepository> _participantRepository;
    private Lazy<IEventRepository> _eventRepository;
    public RepositoryManager(EventsDBContext eventsDBContext)
    {
        _eventsDBContext = eventsDBContext;

        _participantRepository = new Lazy<IParticipantRepository>(() => 
        new ParticipantRepository(eventsDBContext));

        _eventRepository = new Lazy<IEventRepository>(() => 
        new EventRepository(eventsDBContext));
    }
    public IParticipantRepository Participant => _participantRepository.Value;
	public IEventRepository Event => _eventRepository.Value;
    public async Task SaveAsync() => await _eventsDBContext.SaveChangesAsync();
}
