using booking_system.Models;

namespace booking_system.Services;

public interface IAuthService
{
    Task<string> Login(string email, string password);
}