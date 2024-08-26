using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.User.Interfaces;

public interface IGetAllUsersUseCase
{
	Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(Paging paging, bool trackChanges);
}
