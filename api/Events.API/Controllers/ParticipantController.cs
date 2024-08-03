using Events.Application.Services.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Events.API.Controllers;

[ApiController]
[Route("api/{eventId:guid}/participants")]
[Authorize]
public class ParticipantController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ParticipantController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetParticipants(Guid eventId)
	{
        var participants = await _serviceManager
            .ParticipantService
            .GetAllParticipantsAsync(eventId, trackChanges: false);
        
        return Ok(participants);
	}

    [HttpGet("{id:guid}", Name = "ParticipantById")]
	[ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetParticipant(Guid eventId, Guid id)
    {
        var participant = await _serviceManager
            .ParticipantService
            .GetParticipantByIdAsync(eventId, id, trackChanges: false);

        return Ok(participant);
    }

    [HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<IActionResult> CreateParticipant(Guid eventId, [FromBody] ParticipantForCreateRequestDto participant)
    {
        Request.Cookies.TryGetValue("cook", out string? token);
        var userId = _serviceManager.JwtProvider.GetUserId(token);

        var participantResponse = await _serviceManager
            .ParticipantService
            .CreateParticipantAsync(eventId, userId, participant, trackChanges: false);

        return CreatedAtRoute("ParticipantById", new { eventId, id = participantResponse.Id }, participantResponse);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteParticipant(Guid eventId, Guid id)
    {
        await _serviceManager
            .ParticipantService
            .DeleteParticipantAsync(eventId, id, trackChanges: false);

        return NoContent();
    }
}
