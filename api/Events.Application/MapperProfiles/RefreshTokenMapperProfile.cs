using AutoMapper;
using Events.Domain.Models;

namespace Events.Application.MapperProfiles;

public class RefreshTokenMapperProfile : Profile
{
    public RefreshTokenMapperProfile()
    {
		CreateMap<RefreshToken, RefreshToken>()
			.ForMember(rt => rt.Id, opt => opt.Ignore())
			.ForMember(rt => rt.UserId, opt => opt.Ignore());
	}
}
