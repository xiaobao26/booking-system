using booking_system.Data;
using booking_system.Dtos;
using booking_system.Models;
using Microsoft.EntityFrameworkCore;

namespace booking_system.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> FindUserByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task<bool> UserExistByEmail(string userEmail)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == userEmail);
    }

    public void AddUser(User newUser)
    {
        _dbContext.Users.Add(newUser);
    }
    
    public void DeleteUser(User targetUser)
    {
        _dbContext.Users.Remove(targetUser);
    }
    
    public void UpdateUser(User targetUser, UserUpdateDto newUserInfo)
    {
        targetUser.Email = newUserInfo.Email;
        targetUser.Password = newUserInfo.Password;
    }
    
    public async Task<List<User>> GetAllUsersAsync()
    {
        var allUsers = await _dbContext.Users.ToListAsync();
        return allUsers;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}