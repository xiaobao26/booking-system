using booking_system.Models;

namespace booking_system.Infrastructure.Authentication;

public interface IJwtService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}