using booking_system.Models;

namespace booking_system.Repositories;

public interface IUserRepository
{
    Task AddUser(User user);
    Task DeleteUserAsync(Guid userId);
    Task UpdateUserAsync(Guid userId, User newUserInfo);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<List<User>> GetAllUsersAsync();
    Task SaveChangesAsync();
}