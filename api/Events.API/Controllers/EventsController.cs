using Events.Application.Services.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public EventsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _serviceManager.EventService.GetAllEventsAsync(trackChanges: false);

        return Ok(events);
    }

    [HttpGet("{id:guid}", Name = "EventById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        var eventResponse = await _serviceManager.EventService.GetEventByIdAsync(id, trackChanges: false);

        return Ok(eventResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateEvent([FromBody] EventForCreateRequestDto eventModel)
    {
        var eventResponse = await _serviceManager.EventService.CreateEventAsync(eventModel);

        return CreatedAtRoute("EventById", new { id = eventResponse.Id }, eventResponse);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventForUpdateRequestDto eventModel)
    {
        await _serviceManager.EventService.UpdateEventAsync(id, eventModel, trackChanges: true);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await _serviceManager.EventService.DeleteEventAsync(id, trackChanges: false);

        return NoContent();
    }
}
