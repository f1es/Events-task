using AutoMapper;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Enums;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.UserUsecases.Implementations;

public class RegisterUserUseCase : IRegisterUserUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	private readonly ICreateRefreshTokenUseCase _createRefreshTokenUseCase;
	private readonly IGenerateHashUseCase _generateHashUseCase;
	public RegisterUserUseCase(
		IRepositoryManager repositoryManager,
		IMapper mapper,
		ICreateRefreshTokenUseCase createRefreshTokenUseCase,
		IGenerateHashUseCase generateHashUseCase)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
		_createRefreshTokenUseCase = createRefreshTokenUseCase;
		_generateHashUseCase = generateHashUseCase;
	}

	public async Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges)
	{
		var existUser = await _repositoryManager.User.GetByUsernameAsync(user.Username, trackChanges);
		if (existUser != null)
		{
			throw new AlreadyExistsException($"user with username {user.Username} already exist");
		}

		var passwordHash = _generateHashUseCase.GenerateHash(user.Password);
		var userModel = _mapper.Map<User>(user);
		userModel.Id = Guid.NewGuid();

		userModel.PasswordHash = passwordHash;

		userModel.Role = Roles.user.ToString();

		_repositoryManager.User.Create(userModel);
		await _createRefreshTokenUseCase.CreateRefreshTokenAsync(userModel.Id);

		await _repositoryManager.SaveAsync();
	}
}
