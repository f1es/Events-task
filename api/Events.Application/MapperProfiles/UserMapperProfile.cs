using AutoMapper;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
		CreateMap<UserRegisterRequestDto, User>()
			.ForMember(u => u.PasswordHash, opt => opt.Ignore());

		CreateMap<User, UserResponseDto>();
	}
}
