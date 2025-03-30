using booking_system.Infrastructure.Authentication;
using booking_system.Models;
using booking_system.Repositories;

namespace booking_system.Services;

public class AuthService: IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public AuthService(IJwtService jwtService, IUserRepository userRepository)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
    }
    
    public async Task<string> Login(string email, string password)
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

        string token = _jwtService.GenerateJwtToken(targetUser);
        return token;
    }
}