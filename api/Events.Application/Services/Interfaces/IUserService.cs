using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Services.Interfaces;

public interface IUserService
{
	public Task<User> RegisterUser(UserRegisterRequestDto user);
	public Task<string> LoginUser(UserLoginRequestDto user);
	
}
