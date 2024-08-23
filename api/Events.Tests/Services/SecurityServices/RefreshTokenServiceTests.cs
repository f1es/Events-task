using AutoMapper;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.SecurityServices.Implementations;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Domain.Models;
using Moq;

namespace Events.Tests.Services.SecurityServices;

public class RefreshTokenServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IRefreshProvider> _refreshProviderMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly RefreshTokenService _refreshTokenService;
    public RefreshTokenServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _refreshProviderMock = new Mock<IRefreshProvider>();
        _jwtProviderMock = new Mock<IJwtProvider>();
        _mapperMock = new Mock<IMapper>();
        _refreshTokenService = new RefreshTokenService(
            _repositoryManagerMock.Object,
            _refreshProviderMock.Object,
            _jwtProviderMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async void CreateRefreshTokenAsync_ReturnsTask()
    {
        // Arrange
        _refreshProviderMock.Setup(r => 
        r.GenerateToken(It.IsAny<Guid>()))
            .Returns(It.IsAny<RefreshToken>());

        _repositoryManagerMock.Setup(r =>
        r.RefreshToken.CreateRefreshToken(It.IsAny<RefreshToken>()));

        // Act
        await _refreshTokenService.CreateRefreshTokenAsync(It.IsAny<Guid>());

        // Assert
        _refreshProviderMock.Verify(r =>
		r.GenerateToken(It.IsAny<Guid>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.RefreshToken.CreateRefreshToken(It.IsAny<RefreshToken>()), Times.Once);
    }

    [Fact]
    public async void UpdateRefreshToken_ReturnsVoid()
    {
        // Arrange
        var trackChanges = true;

        _repositoryManagerMock.Setup(r =>
        r.RefreshToken.GetRefreshTokenByUserIdAsync(It.IsAny<Guid>(), trackChanges))
            .ReturnsAsync(It.IsAny<RefreshToken>());

        _mapperMock.Setup(m =>
        m.Map(It.IsAny<RefreshToken>(), It.IsAny<RefreshToken>()))
            .Returns(It.IsAny<RefreshToken>());

        // Act 
        await _refreshTokenService.UpdateRefreshToken(
            It.IsAny<Guid>(), 
            It.IsAny<RefreshToken>(),
			trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.RefreshToken.GetRefreshTokenByUserIdAsync(It.IsAny<Guid>(), trackChanges), Times.Once);

        _mapperMock.Verify(m =>
		m.Map(It.IsAny<RefreshToken>(), It.IsAny<RefreshToken>()), Times.Once);
    }

    [Fact]
    public async void RefreshTokensFromTokenValue_ReturnsTupleOfStringAndRefreshToken()
    {
		// Arrange
		var tokenId = Guid.NewGuid();
        var tokenValue = "123";
		var refreshToken = new RefreshToken
		{
			Id = tokenId,
			Expires = DateTime.UtcNow.AddDays(123),
            Token = tokenValue,

		};
        var trackChanges = true;

		_repositoryManagerMock.Setup(r =>
        r.RefreshToken.GetRefreshTokenByValueAsync(It.IsAny<string>(), trackChanges))
            .ReturnsAsync(refreshToken);

        _refreshProviderMock.Setup(r =>
        r.GenerateToken(It.IsAny<Guid>()))
            .Returns(It.IsAny<RefreshToken>());

        _repositoryManagerMock.Setup(r => 
        r.User.GetByIdAsync(It.IsAny<Guid>(), trackChanges))
            .ReturnsAsync(It.IsAny<User>());

        _jwtProviderMock.Setup(j => 
        j.GenerateToken(It.IsAny<User>()))
            .Returns(It.IsAny<string>());

        // Act
        await _refreshTokenService.RefreshTokensFromTokenValue(tokenValue, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.RefreshToken.GetRefreshTokenByValueAsync(It.IsAny<string>(), trackChanges), Times.Once);

        _refreshProviderMock.Verify(r =>
        r.GenerateToken(It.IsAny<Guid>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.User.GetByIdAsync(It.IsAny<Guid>(), trackChanges), Times.Once);

        _jwtProviderMock.Verify(j =>
		j.GenerateToken(It.IsAny<User>()), Times.Once);
    }
}
