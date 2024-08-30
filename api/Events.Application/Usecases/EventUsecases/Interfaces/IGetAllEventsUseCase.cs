using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Interfaces;

public interface IGetAllEventsUseCase
{
    Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(
        EventFilter eventFilter,
        Paging paging,
        bool trackChanges);
}
