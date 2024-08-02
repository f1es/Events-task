using Events.Application.Services.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Mvc;

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
        var token = await _serviceManager.UserService.LoginUserAsync(user, trackChanges: false);

        Response.Cookies.Append("cook", token);

        return Ok();
    }
}
