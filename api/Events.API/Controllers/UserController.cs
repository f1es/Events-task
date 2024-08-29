using Events.API.Extensions;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserUseCaseManager _userUseCaseManager;
    private readonly IRefreshTokenUseCaseManager _refreshTokenUseCaseManager;
    public UserController(
        IUserUseCaseManager userUseCaseManager,
		IRefreshTokenUseCaseManager refreshTokenUseCaseManager)
    {
        _userUseCaseManager = userUseCaseManager;
        _refreshTokenUseCaseManager = refreshTokenUseCaseManager;
    }

    [HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]

	public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto user)
    {
        await _userUseCaseManager.RegisterUserUseCase.RegisterUserAsync(user, trackChanges: false);

        return Created();
    }

    [HttpPost("login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto user)
    {
        var tokens = await _userUseCaseManager.LoginUserUseCase.LoginUserAsync(
            user,
            trackUserChanges: false, 
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

        var tokens = await _refreshTokenUseCaseManager
            .RefreshTokenFromTokenValueUseCase
            .RefreshTokensFromTokenValue(refreshTokenValue, trackChanges: false);


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
        await _userUseCaseManager.GrantRoleForUserUseCase.GrantRoleForUserAsync(
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
        var user = await _userUseCaseManager.GetUserByIdUseCase.GetUserByIdAsync(id, trackChanges: false);

        return Ok(user);
    }
    [HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetUsers([FromQuery] Paging paging)
    {
        var users = await _userUseCaseManager.GetAllUsersUseCase.GetAllUsersAsync(paging, trackChanges: false);

        return Ok(users);
    }
}
