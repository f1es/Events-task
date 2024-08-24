using AutoMapper;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.MapperProfiles;

public class ParticipantMapperProfile : Profile
{
    public ParticipantMapperProfile()
    {
		CreateMap<ParticipantRequestDto, Participant>();

		CreateMap<Participant, ParticipantResponseDto>();
	}
}
