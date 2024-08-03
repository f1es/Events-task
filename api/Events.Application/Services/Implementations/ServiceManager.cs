using AutoMapper;
using Events.Application.JWT.Implementations;
using Events.Application.JWT.Interfaces;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Events.Application.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IEventService> _eventService;
    private readonly Lazy<IParticipantService> _participantService;
    private readonly Lazy<IImageService> _imageService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IJwtProvider> _jwtProvider;
    private readonly Lazy<IPasswordHasher> _passwordHasher;
    public ServiceManager(
        IRepositoryManager repositoryManager,
        IMapper mapper,
        IOptions<JwtOptions> jwtOptions)
    {
        _eventService = new Lazy<IEventService>(() =>
        new EventService(repositoryManager, mapper));

        _participantService = new Lazy<IParticipantService>(() =>
        new ParticipantService(repositoryManager, mapper));

        _jwtProvider = new Lazy<IJwtProvider>(() =>
        new JwtProvider(jwtOptions));

        _passwordHasher = new Lazy<IPasswordHasher>(() =>
        new PasswordHasher());

        _userService = new Lazy<IUserService>(() =>
        new UserService(
            repositoryManager, 
            PasswordHasher, 
            mapper, 
            JwtProvider));

    }

    public IEventService EventService => _eventService.Value;
	public IParticipantService ParticipantService => _participantService.Value;
	public IImageService ImageService => throw new NotImplementedException();
	public IUserService UserService => _userService.Value;
    public IJwtProvider JwtProvider => _jwtProvider.Value;
    public IPasswordHasher PasswordHasher => _passwordHasher.Value;
}
