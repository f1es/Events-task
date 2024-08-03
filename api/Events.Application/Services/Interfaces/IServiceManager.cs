﻿using Events.Application.JWT.Interfaces;

namespace Events.Application.Services.Interfaces;

public interface IServiceManager
{
	IEventService EventService { get; }
	IParticipantService ParticipantService { get; }
	IImageService ImageService { get; }
	IUserService UserService { get; }
	IJwtProvider JwtProvider { get; }
}
