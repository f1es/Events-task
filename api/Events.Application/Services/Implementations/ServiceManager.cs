using AutoMapper;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Interfaces;

namespace Events.Application.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IEventService> _eventService;
    private readonly Lazy<IParticipantService> _participantService;
    private readonly Lazy<IImageService> _imageService;
    private readonly Lazy<IUserService> _userService;
    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _eventService = new Lazy<IEventService>(() =>
        new EventService(repositoryManager, mapper));

        _participantService = new Lazy<IParticipantService>(() =>
        new ParticipantService(repositoryManager, mapper));
    }

    public IEventService EventService => _eventService.Value;

	public IParticipantService ParticipantService => _participantService.Value;

	public IImageService ImageService => throw new NotImplementedException();

	public IUserService UserService => throw new NotImplementedException();
}
