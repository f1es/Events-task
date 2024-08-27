using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.UserUsecases.Interfaces;

public interface IGrantRoleForUserUseCase
{
	public Task GrantRoleForUserAsync(
		Guid id, 
		string role, 
		bool trackChanges);
}
