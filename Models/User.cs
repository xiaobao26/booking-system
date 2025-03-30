using booking_system.Models.Enum;

namespace booking_system.Models;

public class User
{
    public Guid Id { get;  } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EmailVerified { get; set; }
}