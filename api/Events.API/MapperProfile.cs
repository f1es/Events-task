using AutoMapper;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.API;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<EventForCreateRequestDto, Event>();

        CreateMap<EventForUpdateRequestDto, Event>();

        CreateMap<Event, EventResponseDto>();

        CreateMap<ParticipantForCreateRequestDto, Participant>();

        CreateMap<Participant, ParticipantResponseDto>();
    }
}
