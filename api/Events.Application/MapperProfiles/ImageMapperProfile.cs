using AutoMapper;
using Events.Application.Extensions;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Events.Application.MapperProfiles;

public class ImageMapperProfile : Profile
{
    public ImageMapperProfile()
    {
		CreateMap<IFormFile, Image>()
			.ForMember(i => i.Name, opt => opt.MapFrom(f => f.FileName))
			.ForMember(i => i.Type, opt => opt.MapFrom(f => f.ContentType))
			.ForMember(i => i.Content, opt => opt.MapFrom(f => f.OpenReadStream().ToByteArray()));

		CreateMap<Image, ImageResponseDto>();
	}
}
