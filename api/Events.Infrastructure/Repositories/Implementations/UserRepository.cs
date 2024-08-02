using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly EventsDBContext _eventsDBContext;
    public UserRepository(EventsDBContext eventsDBContext)
        :base(eventsDBContext)
    {
        _eventsDBContext = eventsDBContext;
    }
	public async Task<User> GetByIdAsync(Guid id, bool trackChanges) =>
		await GetByPredicate(u => u.Id.Equals(id), trackChanges)
		.SingleOrDefaultAsync();

	public async Task<User> GetByUsernameAsync(string username, bool trackChanges) => 
		await GetByPredicate(u => u.Username.Equals(username), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateUser(User user) =>
		Create(user);

	public void DeleteUser(User user) => 
		Delete(user);


	public void UpdateUser(User user) => 
		Update(user);
}
