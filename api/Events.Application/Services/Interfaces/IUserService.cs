using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Services.Interfaces;

public interface IUserService
{
	public Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges);
	public Task<string> LoginUserAsync(UserLoginRequestDto user, bool trackChanges);
	
}
