using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Infrastructure.Context;
using Events.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories.Implementations;

public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventsDBContext eventsDBContext)
        : base(eventsDBContext)
    {
        
    }

	public async Task<IEnumerable<Participant>> GetAllAsync(Guid eventId, Paging paging, bool trackChanges)
	{
		var participantsQuery = GetByPredicate(p => p.EventId.Equals(eventId), trackChanges);

		participantsQuery = participantsQuery.Paginate(paging.Page, paging.PageSize);

		participantsQuery = participantsQuery.OrderBy(p => p.Name);

		return await participantsQuery.ToListAsync();
	}

	public async Task<Participant> GetByIdAsync(Guid id, bool trackChanges) => 
		await GetByPredicate(p => p.Id.Equals(id), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateParticipant(Guid eventId, Guid userId, Participant participant)
	{
		participant.UserId = userId;
		participant.EventId = eventId;
		Create(participant);
	}
	public void DeleteParticipant(Participant participant) =>
		Delete(participant);

	public void UpdateParticipant(Participant participant) => 
		Update(participant);
}
