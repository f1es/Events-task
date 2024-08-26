using Events.API.Attributes;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Enums;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/events")]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public EventsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetEvents([FromQuery] EventFilter eventFilter, [FromQuery] Paging paging)
    {
        var events = await _serviceManager.EventService.GetAllEventsAsync(eventFilter, paging, trackChanges: false);

        return Ok(events);
    }

    [HttpGet("{id:guid}", Name = "EventById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetEvent(Guid id)
    {
        var eventResponse = await _serviceManager.EventService.GetEventByIdAsync(id, trackChanges: false);

        return Ok(eventResponse);
    }

    [HttpPost]
    [RequiredRole("Admin, Manager")]
    [ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateEvent([FromBody] EventRequestDto eventModel)
    {
        var eventResponse = await _serviceManager.EventService.CreateEventAsync(eventModel);

        return CreatedAtRoute("EventById", new { id = eventResponse.Id }, eventResponse);
    }

    [HttpPut("{id:guid}")]
	[RequiredRole("Admin, Manager")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventRequestDto eventModel)
    {
        await _serviceManager.EventService.UpdateEventAsync(id, eventModel, trackChanges: true);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await _serviceManager.EventService.DeleteEventAsync(id, trackChanges: false);

        return NoContent();
    }
}
