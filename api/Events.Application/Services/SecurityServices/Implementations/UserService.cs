using AutoMapper;
using Events.Application.Extensions;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Domain.Enums;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using FluentValidation;

namespace Events.Application.Services.SecurityServices.Implementations;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshProvider _refreshProvider;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IValidator<UserLoginRequestDto> _loginValidator;
    private readonly IValidator<UserRegisterRequestDto> _registerValidator;
    public UserService(
        IRepositoryManager repositoryManager,
        IPasswordHasher passwordHasher,
        IMapper mapper,
        IJwtProvider jwtProvider,
        IRefreshProvider refreshProvider,
        IRefreshTokenService refreshTokenService,
        IValidator<UserLoginRequestDto> loginValidator,
        IValidator<UserRegisterRequestDto> registerValidator)
    {
        _repositoryManager = repositoryManager;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _refreshProvider = refreshProvider;
        _refreshTokenService = refreshTokenService;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }
    public async Task<(string accessToken, RefreshToken refreshToken)> LoginUserAsync(
        UserLoginRequestDto user,
        bool trackUsernameChanges,
        bool trackRefreshTokenChanges)
    {
        var validationResult = _loginValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            throw new InvalidModelException(validationResult.GetMessage());
        }

        var userModel = await _repositoryManager.User.GetByUsernameAsync(user.Username, trackUsernameChanges);
        if (userModel is null)
        {
            throw new NotFoundException($"user with username {user.Username} not found");
        }

        var verificationResult = _passwordHasher.VerifyPassword(user.Password, userModel.PasswordHash);
        if (!verificationResult)
        {
            throw new FailedToLoginException($"Failed to login");
        }

        var accessToken = _jwtProvider.GenerateToken(userModel);
        var refreshToken = _refreshProvider.GenerateToken(userModel.Id);

        await _refreshTokenService.UpdateRefreshToken(
            userModel.Id,
            refreshToken,
            trackRefreshTokenChanges);


        return (accessToken, refreshToken);
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
        
        userModel.Role = Roles.user.ToString();

        _repositoryManager.User.CreateUser(userModel);
        await _refreshTokenService.CreateRefreshTokenAsync(userModel.Id);

        await _repositoryManager.SaveAsync();
    }

    public async Task GrantRole(Guid id, string role, bool trackChanges)
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

        switch(role)
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

    public async Task<User> GetByIdAsync(Guid id, bool trackChanges)
    {
        var user = await _repositoryManager.User.GetByIdAsync(id, trackChanges);
        if (user is null)
        {
            throw new NotFoundException($"user with id {id} not found");
        }

        return user;
    }
}
