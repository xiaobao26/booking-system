using booking_system.Models;

namespace booking_system.Services;

public interface IAuthService
{
    Task<object> Login(string email, string password);
    Task<object> LoginWithRefreshToken(string token);
}