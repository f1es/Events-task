using AutoMapper;
using Events.Application.JWT.Implementations;
using Events.Application.JWT.Interfaces;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Interfaces;
using Events.Domain.Shared.DTO.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
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
        IOptions<JwtOptions> jwtOptions,
        IValidator<EventForCreateRequestDto> eventCreateValidator,
        IValidator<EventForUpdateRequestDto> eventUpdateValidator,
        IValidator<ParticipantForCreateRequestDto> participantCreateValidator,
        IValidator<ParticipantForUpdateRequestDto> participantForUpdateValidator,
        IValidator<UserLoginRequestDto> userLoginValidator,
        IValidator<UserRegisterRequestDto> userRegisterValidator,
        IValidator<IFormFile> imageValidator)
    {
        _eventService = new Lazy<IEventService>(() =>
        new EventService(
        repositoryManager,
        mapper,
        eventCreateValidator,
        eventUpdateValidator));

        _participantService = new Lazy<IParticipantService>(() =>
        new ParticipantService(
        repositoryManager,
        mapper,
        participantForUpdateValidator,
        participantCreateValidator));

        _imageService = new Lazy<IImageService>(() =>
        new ImageService(repositoryManager, mapper, imageValidator));

        _jwtProvider = new Lazy<IJwtProvider>(() =>
        new JwtProvider(jwtOptions));

        _passwordHasher = new Lazy<IPasswordHasher>(() =>
        new PasswordHasher());

        _userService = new Lazy<IUserService>(() =>
        new UserService(
            repositoryManager, 
            PasswordHasher, 
            mapper, 
            JwtProvider,
            userLoginValidator,
            userRegisterValidator));
    }

    public IEventService EventService => _eventService.Value;
	public IParticipantService ParticipantService => _participantService.Value;
	public IImageService ImageService => _imageService.Value;
	public IUserService UserService => _userService.Value;
    public IJwtProvider JwtProvider => _jwtProvider.Value;
    public IPasswordHasher PasswordHasher => _passwordHasher.Value;
}
