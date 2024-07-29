using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;

namespace Events.Infrastructure.Repositories.Implementations;

public class RepositoryManager : IRepositoryManager
{
	private readonly EventsDBContext _eventsDBContext;
    private Lazy<IParticipantRepository> _participantRepository;
    private Lazy<IEventRepository> _eventRepository;
    private Lazy<IUserRepository> _userRepository;
    private Lazy<IImageRepository> _imageRepository;
    public RepositoryManager(EventsDBContext eventsDBContext)
    {
        _eventsDBContext = eventsDBContext;

        _participantRepository = new Lazy<IParticipantRepository>(() => 
        new ParticipantRepository(eventsDBContext));

        _eventRepository = new Lazy<IEventRepository>(() => 
        new EventRepository(eventsDBContext));

        _userRepository = new Lazy<IUserRepository>(() =>
        new UserRepository(eventsDBContext));

        _imageRepository = new Lazy<IImageRepository>(() =>
        new ImageRepository(eventsDBContext));
    }
    public IParticipantRepository Participant => _participantRepository.Value;
	public IEventRepository Event => _eventRepository.Value;
    public IUserRepository User => _userRepository.Value;
    public IImageRepository Image => _imageRepository.Value;
    public async Task SaveAsync() => await _eventsDBContext.SaveChangesAsync();
}
