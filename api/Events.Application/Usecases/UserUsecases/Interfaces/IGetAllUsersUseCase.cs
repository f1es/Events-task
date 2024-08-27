using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.UserUsecases.Interfaces;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(Paging paging, bool trackChanges);
}
