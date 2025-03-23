using booking_system.Data;
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

    private async Task<User?> FindUserById(Guid userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    private async Task<bool> FindUserByEmail(string userEmail)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == userEmail);
    }

    public async Task AddUser(User user)
    {
        var isUserExist = await FindUserByEmail(user.Email);
        if (isUserExist) throw new Exception("User already exist");
        
        _dbContext.Users.Add(user);
    }
    
    public async Task DeleteUserAsync(Guid userId)
    {
        var targetUser = await FindUserById(userId);
        if (targetUser == null) throw new Exception("User cannot found");
        _dbContext.Users.Remove(targetUser);
    }
    
    public async Task UpdateUserAsync(Guid userId, User newUserInfo)
    {
        var targetUser = await FindUserById(userId);
        if (targetUser == null) throw new Exception("User cannot found");

        targetUser.Email = newUserInfo.Email;
        targetUser.Password = newUserInfo.Password;
    }
    
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await FindUserById(userId);
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