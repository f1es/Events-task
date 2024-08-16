﻿using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Services.SecurityServices.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> GetUserByIdAsync(Guid id, bool trackChanges);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(Paging paging, bool trackChanges);
    Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges);
    Task<(string accessToken, RefreshToken refreshToken)> LoginUserAsync(
        UserLoginRequestDto user,
        bool trackUsernameChanges,
        bool trackRefreshTokenChanges);
    Task GrantRoleForUser(Guid id, string role, bool trackChanges);
}
