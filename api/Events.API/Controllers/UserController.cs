using Events.API.Attributes;
using Events.API.Extensions;
using Events.Application.Extensions;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Shared;
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
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]

	public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto user)
    {
        await _serviceManager.UserService.RegisterUserAsync(user, trackChanges: false);

        return Created();
    }

    [HttpPost("login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GrantRoleForUser([FromBody] GrantRoleDto grantRole)
    {
        await _serviceManager.UserService.GrantRoleForUser(
            grantRole.UserId, 
            grantRole.Role, 
            trackChanges: true);

        return Ok();
    }
    [HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _serviceManager.UserService.GetUserByIdAsync(id, trackChanges: false);

        return Ok(user);
    }
    [HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetUsers([FromQuery] Paging paging)
    {
        var users = await _serviceManager.UserService.GetAllUsersAsync(paging, trackChanges: false);

        return Ok(users);
    }
}
