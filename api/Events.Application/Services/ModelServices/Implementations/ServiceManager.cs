using AutoMapper;
using Events.Application.Options;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Application.Services.SecurityServices.Implementations;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Domain.Shared.DTO.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Events.Application.Services.ModelServices.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IEventService> _eventService;
    private readonly Lazy<IParticipantService> _participantService;
    private readonly Lazy<IImageService> _imageService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IRefreshTokenService> _refreshTokenService;
    private readonly Lazy<IJwtProvider> _jwtProvider;
    private readonly Lazy<IRefreshProvider> _refreshProvider;
    private readonly Lazy<IPasswordHasher> _passwordHasher;
    public ServiceManager(
        IRepositoryManager repositoryManager,
        IMapper mapper,
        IOptions<JwtOptions> jwtOptions,
        IOptions<RefreshTokenOptions> refreshTokenOptions)
    {
        _eventService = new Lazy<IEventService>(() =>
        new EventService(
        repositoryManager,
        mapper));

        _participantService = new Lazy<IParticipantService>(() =>
        new ParticipantService(
        repositoryManager,
        mapper));

        _refreshTokenService = new Lazy<IRefreshTokenService>(() =>
        new RefreshTokenService(repositoryManager, RefreshProvider, JwtProvider, mapper));

        _imageService = new Lazy<IImageService>(() =>
        new ImageService(repositoryManager, mapper));

        _refreshProvider = new Lazy<IRefreshProvider>(() =>
        new RefreshProvider(refreshTokenOptions));

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
            RefreshProvider,
            RefreshTokenService));
    }

    public IEventService EventService => _eventService.Value;
    public IParticipantService ParticipantService => _participantService.Value;
    public IImageService ImageService => _imageService.Value;
    public IUserService UserService => _userService.Value;
    public IRefreshTokenService RefreshTokenService => _refreshTokenService.Value;
    public IJwtProvider JwtProvider => _jwtProvider.Value;
    public IRefreshProvider RefreshProvider => _refreshProvider.Value;
    public IPasswordHasher PasswordHasher => _passwordHasher.Value;
}
