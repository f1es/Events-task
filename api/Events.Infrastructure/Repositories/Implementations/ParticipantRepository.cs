using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Infrastructure.Context;
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

		participantsQuery = Paginate(participantsQuery, paging.Page, paging.PageSize);

		participantsQuery.OrderBy(p => p.Name);

		return await participantsQuery.ToListAsync();
	}

	private IQueryable<Participant> Paginate(IQueryable<Participant> participants, int page, int pageSize)
	{
		participants = participants
		.Skip((page - 1) * pageSize)
		.Take(pageSize);

		return participants;
	}

	public async Task<Participant> GetByIdAsync(Guid eventId, Guid id, bool trackChanges) => 
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
