using AutoMapper;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;

namespace Events.Application.Services.SecurityServices.Implementations;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IRefreshProvider _refreshProvider;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    public RefreshTokenService(
        IRepositoryManager repositoryManager,
        IRefreshProvider refreshProvider,
        IJwtProvider jwtProvider,
        IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _refreshProvider = refreshProvider;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task CreateRefreshTokenAsync(Guid userId)
    {
        var token = _refreshProvider.GenerateToken(userId);

        _repositoryManager.RefreshToken.CreateRefreshToken(token);

        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateRefreshToken(
        Guid userId,
        RefreshToken refreshToken,
        bool trackChanges)
    {
        var userToken = await _repositoryManager.RefreshToken.GetRefreshTokenByUserIdAsync(userId, trackChanges);

        userToken = _mapper.Map(refreshToken, userToken);

        await _repositoryManager.SaveAsync();
    }

    public async Task<(string accessToken, RefreshToken refreshToken)> RefreshTokensFromTokenValue(
        string refreshTokenValue,
        bool trackChanges)
    {
        var refreshToken = await _repositoryManager.RefreshToken.GetRefreshTokenByValueAsync(refreshTokenValue, trackChanges);

        if (!ValidateRefreshToken(refreshToken, refreshTokenValue, trackChanges))
        {
            throw new InvalidRefreshTokenException("Refresh token is invalid");
        }

        var userId = refreshToken.UserId;

        var newRefreshToken = _refreshProvider.GenerateToken(userId);
        await UpdateRefreshToken(userId, newRefreshToken, trackChanges);

        var user = await _repositoryManager.User.GetByIdAsync(userId, trackChanges);
        var accessToken = _jwtProvider.GenerateToken(user);

        return (accessToken, newRefreshToken);
    }

    private bool ValidateRefreshToken(
        RefreshToken refreshToken,
        string refreshTokenValue,
        bool trackChanges)
    {

        if (refreshToken is null)
            return false;

        if (!refreshToken.Token.Equals(refreshTokenValue))
            return false;

        if (refreshToken.Expires < DateTime.UtcNow)
            return false;

        return true;
    }
}
