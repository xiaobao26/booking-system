using booking_system.Dtos;
using booking_system.Models;

namespace booking_system.Repositories;

public interface IUserRepository
{
    Task<User?> FindUserByIdAsync(Guid userId);
    Task<bool> UserExistByEmail(string userEmail);
    void AddUser(User newUser);
    void DeleteUser(User targetUser);
    void UpdateUser(User targetUser, UserUpdateDto newUserInfo);
    Task<List<User>> GetAllUsersAsync();
    Task SaveChangesAsync();
}