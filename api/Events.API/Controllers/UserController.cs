using Events.API.Attributes;
using Events.API.Extensions;
using Events.Application.Extensions;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Events.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto user)
    {
        await _serviceManager.UserService.RegisterUserAsync(user, trackChanges: false);

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto user)
    {
        var tokens = await _serviceManager.UserService.LoginUserAsync(
            user, 
            trackUsernameChanges: false,
            trackRefreshTokenChanges: true);

        Response.AppendAccessToken(tokens.accessToken);
        Response.AppendRefreshToken(tokens.refreshToken);

		return Ok();
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshUserTokens()
    {
        var refreshTokenValue = Request.GetRefreshToken();

        var tokens = await _serviceManager
            .RefreshTokenService
            .RefreshTokensFromTokenValue(refreshTokenValue, trackChanges: true);

        Response.AppendAccessToken(tokens.accessToken);
        Response.AppendRefreshToken(tokens.refreshToken);

		return Ok();
    }
	[HttpPost("grant-role")]
	[RequiredRole("admin")]
    public async Task<IActionResult> GrantRoleForUser([FromBody] GrantRoleDto grantRole)
    {
        await _serviceManager.UserService.GrantRole(
            grantRole.UserId, 
            grantRole.Role, 
            trackChanges: true);

        return Ok();
    }
}
