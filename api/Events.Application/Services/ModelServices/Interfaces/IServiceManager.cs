using Events.Application.Services.SecurityServices.Interfaces;

namespace Events.Application.Services.ModelServices.Interfaces;

public interface IServiceManager
{
    IEventService EventService { get; }
    IParticipantService ParticipantService { get; }
    IImageService ImageService { get; }
    IUserService UserService { get; }
    IRefreshTokenService RefreshTokenService { get; }
    IJwtProvider JwtProvider { get; }
    IRefreshProvider RefreshProvider { get; }
}
