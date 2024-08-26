using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
	Task<User> GetByIdAsync(Guid id, bool trackChanges);
	Task<User> GetByUsernameAsync(string username, bool trackChanges);
	Task<IEnumerable<User>> GetAllAsync(Paging paging, bool trackChanges);
}

