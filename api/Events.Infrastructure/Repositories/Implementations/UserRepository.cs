using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;

namespace Events.Infrastructure.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly EventsDBContext _eventsDBContext;
    public UserRepository(EventsDBContext eventsDBContext)
        :base(eventsDBContext)
    {
        _eventsDBContext = eventsDBContext;
    }

}
