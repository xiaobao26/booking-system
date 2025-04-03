using booking_system.Data;
using booking_system.Infrastructure.Authentication;
using booking_system.Models;
using booking_system.Repositories;
using Microsoft.EntityFrameworkCore;

namespace booking_system.Services;

public class AuthService: IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly ApplicationDbContext _dbContext;

    public AuthService(IJwtService jwtService, IUserRepository userRepository, ApplicationDbContext dbContext)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _dbContext = dbContext;
    }
    
    public async Task<object> Login(string email, string password)
    {
        var targetUser = await _userRepository.FindUserByEmailAsync(email);

        if (targetUser == null)
        {
            throw new Exception("User cannot be found");
        }

        if (targetUser.Password != password)
        {
            throw new Exception("User password incorrect.");
        }

        string accessToken = _jwtService.GenerateJwtToken(targetUser);
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = _jwtService.GenerateRefreshToken(),
            UserId = targetUser.Id,
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
        };
        
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();
        
        return new { 
            AccessToken= accessToken, 
            RefreshToken = refreshToken.Token 
        };
    }

    public async Task<object> LoginWithRefreshToken(string token)
    {
        RefreshToken? refreshToken = await _dbContext.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == token);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            throw new ApplicationException("The refresh token has expired.");
        }
        
        // remove the old refreshToken
        _dbContext.RefreshTokens.Remove(refreshToken);
        
        // generate new accessToken and new refreshToken
        string accessToken = _jwtService.GenerateJwtToken(refreshToken.User);
        RefreshToken newRefreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = refreshToken.UserId,
            Token = _jwtService.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
        };
        _dbContext.RefreshTokens.Add(newRefreshToken);
        await _dbContext.SaveChangesAsync();

        return new
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
        };
    }
   
}