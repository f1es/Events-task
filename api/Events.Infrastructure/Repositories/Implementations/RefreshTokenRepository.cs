﻿using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories.Implementations;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(EventsDBContext eventsDBContext)
		: base(eventsDBContext)
    {
        
    }
	public void CreateRefreshToken(RefreshToken refreshToken) =>
		Create(refreshToken);

	public void DeleteRefreshToken(RefreshToken refreshToken) => 
		Delete(refreshToken);

	public async Task<RefreshToken> GetRefreshTokenAsync(Guid userId, bool trackChanges) => 
		await GetByPredicate(rt => rt.UserId.Equals(userId), trackChanges)
		.FirstOrDefaultAsync();
}
