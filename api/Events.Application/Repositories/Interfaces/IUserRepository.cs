using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IUserRepository 
{
	Task<User> GetByIdAsync(Guid id, bool trackChanges);
	Task<User> GetByUsernameAsync(string username, bool trackChanges);
	void CreateUser(User user);
	void DeleteUser(User user);
	void UpdateUser(User user);

}

