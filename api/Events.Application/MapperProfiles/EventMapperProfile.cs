using AutoMapper;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.MapperProfiles;

public class EventMapperProfile : Profile
{
    public EventMapperProfile()
    {
		CreateMap<EventRequestDto, Event>();

		CreateMap<Event, EventResponseDto>();
	}
}
