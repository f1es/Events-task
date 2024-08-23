namespace Events.Domain.Repositories.Interfaces;

public interface IRepositoryManager
{
	IParticipantRepository Participant { get; }
	IEventRepository Event { get; }
	IUserRepository User { get; }
	IImageRepository Image { get; }
	IRefreshTokenRepository RefreshToken { get; }
	Task SaveAsync();
}
