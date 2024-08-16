using AutoMapper;
using Events.Application.Extensions;
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

        CreateMap<UserRegisterRequestDto, User>()
            .ForMember(u => u.PasswordHash, opt => opt.Ignore());

        CreateMap<User, UserResponseDto>();

        CreateMap<IFormFile, Image>()
            .ForMember(i => i.Name, opt => opt.MapFrom(f => f.FileName))
            .ForMember(i => i.Type, opt => opt.MapFrom(f => f.ContentType))
            .ForMember(i => i.Content, opt => opt.MapFrom(f => f.OpenReadStream().ToByteArray()));

        CreateMap<Image, ImageResponseDto>();

        CreateMap<RefreshToken, RefreshToken>()
            .ForMember(rt => rt.Id, opt => opt.Ignore())
            .ForMember(rt => rt.UserId, opt => opt.Ignore());
    }
}
