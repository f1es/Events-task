using AutoMapper;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.SecurityServices.Implementations;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Events.Tests.Services.SecurityServices;

public class UserServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly Mock<IRefreshProvider> _refreshProviderMock;
    private readonly Mock<IRefreshTokenService> _refreshTokenServiceMock;
    private readonly IUserService _userService;
    public UserServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _mapperMock = new Mock<IMapper>();
        _jwtProviderMock = new Mock<IJwtProvider>();
        _refreshProviderMock = new Mock<IRefreshProvider>();
        _refreshTokenServiceMock = new Mock<IRefreshTokenService>();

        _userService = new UserService(
            _repositoryManagerMock.Object, 
            _passwordHasherMock.Object, 
            _mapperMock.Object, 
            _jwtProviderMock.Object, 
            _refreshProviderMock.Object, 
            _refreshTokenServiceMock.Object);
    }

    [Fact] 
    public async void RegisterUserAsync_ReturnsVoid()
    {
        // Arrange
        var validationResult = new ValidationResult();

        var username = "123";
        var email = "email@email.em";
        var password = "password";

        var userRegisterDto = new UserRegisterRequestDto(
            username, 
            email,
            password);

        var trackChanges = false;

        _repositoryManagerMock.Setup(r => 
        r.User.GetByUsernameAsync(username, trackChanges));

        _passwordHasherMock.Setup(p =>
        p.GenerateHash(It.IsAny<string>()))
            .Returns(It.IsAny<string>());

        var user = new User
        {
            Username = username,
            Email = email,
        };

        _mapperMock.Setup(m => 
        m.Map<User>(It.IsAny<UserRegisterRequestDto>()))
            .Returns(user);

        _repositoryManagerMock.Setup(r => 
        r.User.CreateUser(It.IsAny<User>()));

        _refreshTokenServiceMock.Setup(r => 
        r.CreateRefreshTokenAsync(It.IsAny<Guid>()));

        // Act
        await _userService.RegisterUserAsync(userRegisterDto, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.User.GetByUsernameAsync(It.IsAny<string>(), trackChanges), Times.Once);

        _passwordHasherMock.Verify(p =>
		p.GenerateHash(It.IsAny<string>()), Times.Once);

        _mapperMock.Verify(m =>
		m.Map<User>(It.IsAny<UserRegisterRequestDto>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.User.CreateUser(It.IsAny<User>()), Times.Once);

        _refreshTokenServiceMock.Verify(r =>
		r.CreateRefreshTokenAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async void GrantRoleForUserAsync_ReturnsVoid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
        };
        _repositoryManagerMock.Setup(r =>
        r.User.GetByIdAsync(userId, true))
            .ReturnsAsync(user);
        var role = "user";
        var trackChanges = true;

        // Act
        await _userService.GrantRoleForUserAsync(userId, role, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.User.GetByIdAsync(It.IsAny<Guid>(), trackChanges), Times.Once);
    }

    [Fact]
    public async void GetUserByIdAsync_ReturnsUserResponseDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
        };
        var trackChanges = false;
        _repositoryManagerMock.Setup(r => 
        r.User.GetByIdAsync(userId, trackChanges))
            .ReturnsAsync(user);

        _mapperMock.Setup(m => 
        m.Map<UserResponseDto>(It.IsAny<User>()))
            .Returns(It.IsAny<UserResponseDto>());

		// Act
        await _userService.GetUserByIdAsync(userId, trackChanges);

		// Assert
        _repositoryManagerMock.Verify(r =>
		r.User.GetByIdAsync(userId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
		m.Map<UserResponseDto>(It.IsAny<User>()), Times.Once);
	}

    [Fact]
    public async void GetAllUsersAsync_ReturnsIEnumerableUserResponseDto()
    {
        // Arrange
        var paging = new Paging();
        var trackChanges = false;

        _repositoryManagerMock.Setup(r =>
        r.User.GetAllAsync(paging, trackChanges))
            .ReturnsAsync(It.IsAny<IEnumerable<User>>());

        _mapperMock.Setup(m =>
        m.Map<IEnumerable<UserResponseDto>>(It.IsAny<IEnumerable<User>>()))
            .Returns(It.IsAny<IEnumerable<UserResponseDto>>());

		// Act
        await _userService.GetAllUsersAsync(paging, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.User.GetAllAsync(paging, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
		m.Map<IEnumerable<UserResponseDto>>(It.IsAny<IEnumerable<User>>()), Times.Once);
	}
    [Fact]
    public async void LoginUserAsync_ReturnsVoid()
    {
        // Arrange
        var validationResult = new ValidationResult();

        var trackUserChanges = false;
        var trackRefreshTokenChanges = true;

        var username = "231";

        var user = new User
        {
            Username = username,
        };

		_repositoryManagerMock.Setup(r => 
        r.User.GetByUsernameAsync(username, trackUserChanges))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(p => 
        p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        _jwtProviderMock.Setup(p =>
        p.GenerateToken(user))
            .Returns(It.IsAny<string>());

        _refreshProviderMock.Setup(r =>
        r.GenerateToken(user.Id))
            .Returns(It.IsAny<RefreshToken>());

        _refreshTokenServiceMock.Setup(r => 
        r.UpdateRefreshToken(user.Id, It.IsAny<RefreshToken>(), trackRefreshTokenChanges));

        var password = "123";
        var userLoginDto = new UserLoginRequestDto(username, password);

        // Act
        await _userService.LoginUserAsync(userLoginDto, trackUserChanges, trackRefreshTokenChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.User.GetByUsernameAsync(username, trackUserChanges), Times.Once);

        _passwordHasherMock.Verify(p =>
		p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        _jwtProviderMock.Verify(p =>
		p.GenerateToken(user), Times.Once);

        _refreshProviderMock.Verify(r =>
		r.GenerateToken(user.Id), Times.Once);

        _refreshTokenServiceMock.Verify(r =>
		r.UpdateRefreshToken(user.Id, It.IsAny<RefreshToken>(), trackRefreshTokenChanges), Times.Once);

    }
}
