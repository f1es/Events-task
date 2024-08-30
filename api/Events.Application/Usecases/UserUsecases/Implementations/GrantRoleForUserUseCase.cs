using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Enums;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.UserUsecases.Implementations;

public class GrantRoleForUserUseCase : IGrantRoleForUserUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	public GrantRoleForUserUseCase(IRepositoryManager repositoryManager)
	{
		_repositoryManager = repositoryManager;
	}
	public async Task GrantRoleForUserAsync(Guid id, string role, bool trackChanges)
	{
		var user = await _repositoryManager.User.GetByIdAsync(id, trackChanges);
		if (user is null)
		{
			throw new NotFoundException($"user with id {id} not found");
		}

		var verifiedRole = GetRoleIfExist(role);

		user.Role = verifiedRole;

		await _repositoryManager.SaveAsync();
	}
	private string GetRoleIfExist(string role)
	{
		role = role.ToLower();

		switch (role)
		{
			case nameof(Roles.admin):
				return Roles.admin.ToString();
			case nameof(Roles.user):
				return Roles.user.ToString();
			case nameof(Roles.manager):
				return Roles.manager.ToString();
			default:
				throw new BadRequestException($"Role {role} doesn't exist");
		}
	}
}
