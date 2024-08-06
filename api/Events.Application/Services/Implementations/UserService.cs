using AutoMapper;
using Events.Application.Extensions;
using Events.Application.JWT.Interfaces;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Services.Implementations;

public class UserService : IUserService
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IMapper _mapper;
	private readonly IJwtProvider _jwtProvider;
	private readonly IValidator<UserLoginRequestDto> _loginValidator;
	private readonly IValidator<UserRegisterRequestDto> _registerValidator;
    public UserService(
		IRepositoryManager repositoryManager, 
		IPasswordHasher passwordHasher,
		IMapper mapper,
		IJwtProvider jwtProvider,
		IValidator<UserLoginRequestDto> loginValidator,
		IValidator<UserRegisterRequestDto> registerValidator)
    {
		_repositoryManager = repositoryManager;
		_passwordHasher = passwordHasher;
		_mapper = mapper;
		_jwtProvider = jwtProvider;
		_loginValidator = loginValidator;
		_registerValidator = registerValidator;
    }
    public async Task<string> LoginUserAsync(UserLoginRequestDto user, bool trackChanges)
	{
		var validationResult = _loginValidator.Validate(user);
		if (!validationResult.IsValid)
		{
			throw new InvalidModelException(validationResult.GetMessage());
		}

		var userModel = await _repositoryManager.User.GetByUsernameAsync(user.Username, trackChanges);
		if (userModel is null)
		{
			throw new NotFoundException($"user with username {user.Username} not found");
		}

		var verificationResult = _passwordHasher.VerifyPassword(user.Password, userModel.PasswordHash);
		if (!verificationResult)
		{
			throw new FailedToLoginException($"Failed to login");
		}

		var token = _jwtProvider.GenerateToken(userModel);

		return token;
	}

	public async Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges)
	{
		var validationResult = _registerValidator.Validate(user);
		if (!validationResult.IsValid)
		{
			throw new InvalidModelException(validationResult.GetMessage());
		}

		var existUser = await _repositoryManager.User.GetByUsernameAsync(user.Username, trackChanges);
        if (existUser != null)
        {
			throw new AlreadyExistException($"user with username {user.Username} already exist");
        }

        var passwordHash = _passwordHasher.GenerateHash(user.Password);

		var userModel = _mapper.Map<User>(user);	

		userModel.PasswordHash = passwordHash;

		_repositoryManager.User.CreateUser(userModel);

		await _repositoryManager.SaveAsync();
	}
}
