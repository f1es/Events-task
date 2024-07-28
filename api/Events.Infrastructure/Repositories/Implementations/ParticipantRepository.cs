using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories.Implementations;

public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventsDBContext eventsDBContext)
        : base(eventsDBContext)
    {
        
    }

	public async Task<IEnumerable<Participant>> GetAllAsync() =>
		await GetAll(trackChanges: false)
		.OrderBy(p => p.Name)
		.ToListAsync();

	public async Task<Participant> GetByIdAsync(Guid eventId, Guid id) => 
		await GetByPredicate(p => p.Id.Equals(id), trackChanges: false)
		.SingleOrDefaultAsync();
}
