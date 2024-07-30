using Events.Application.Services.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers
{
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
        public async Task<IActionResult> GetEvents()
        {
            var events = await _serviceManager.EventService.GetAllEventsAsync(trackChanges: false);

            return Ok(events);
        }

        [HttpGet("{id:guid}", Name = "EventById")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var eventResponse = await _serviceManager.EventService.GetEventByIdAsync(id, trackChanges: false);

            return Ok(eventResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventForCreateRequestDto eventModel)
        {
            var eventResponse = await _serviceManager.EventService.CreateEventAsync(eventModel);

            return Ok(eventResponse);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventForUpdateRequestDto eventModel)
        {
            await _serviceManager.EventService.UpdateEventAsync(id, eventModel, trackChanges: true);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _serviceManager.EventService.DeleteEventAsync(id, trackChanges: false);

            return NoContent();
        }
    }
}
