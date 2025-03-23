using System.ComponentModel.DataAnnotations;

namespace booking_system.Dtos;

public class UserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters include uppercase, lowercase, number and special character.")]
    [StringLength(128, ErrorMessage = "Password cannot be longer than 128 characters.")]
    public string Password { get; set; }

    public string Test;
}