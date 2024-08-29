using Events.API.Attributes;
using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/{eventId:guid}/participants")]
[Authorize]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantUseCaseManager _participantUseCaseManager;
    private readonly IJwtProviderUseCaseManager _jwtProviderUseCaseManager;
    public ParticipantController(IParticipantUseCaseManager participantUseCaseManager, IJwtProviderUseCaseManager jwtProviderUseCaseManager)
    {
        _participantUseCaseManager = participantUseCaseManager;
        _jwtProviderUseCaseManager = jwtProviderUseCaseManager;
    }

    [HttpGet]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetParticipants(Guid eventId, [FromQuery] Paging paging)
    {
        var participants = await _participantUseCaseManager
            .GetAllParticipantsUseCase
            .GetAllParticipantsAsync(eventId, paging, trackChanges: false);

        return Ok(participants);
    }

    [HttpGet("{id:guid}", Name = "ParticipantById")]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetParticipant(Guid eventId, Guid id)
    {
		var participant = await _participantUseCaseManager
            .GetParticipantByIdUseCase
            .GetParticipantByIdAsync(eventId, id, trackChanges: false);


		return Ok(participant);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> CreateParticipant(Guid eventId, [FromBody] ParticipantRequestDto participant)
    {
        Request.Cookies.TryGetValue("acc", out string? token);
        var userId = _jwtProviderUseCaseManager.GetUserIdUseCase.GetUserId(token);

		var participantResponse = await _participantUseCaseManager
            .CreateParticipantUseCase
            .CreateParticipantAsync(eventId, userId, participant, trackChanges: false);

		return CreatedAtRoute("ParticipantById", new { eventId, id = participantResponse.Id }, participantResponse);
    }

    [HttpDelete("{id:guid}")]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteParticipant(Guid eventId, Guid id)
    {
        await _participantUseCaseManager
            .DeleteParticipantUseCase
            .DeleteParticipantAsync(eventId, id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdateParticipant(Guid eventId, Guid id, [FromBody] ParticipantRequestDto participant)
    {
        await _participantUseCaseManager
            .UpdateParticipantUseCase
            .UpdateParticipantAsync(eventId, id, participant, trackChanges: true);

        return NoContent();
    }
}
