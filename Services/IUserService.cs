using booking_system.Dtos;

namespace booking_system.Services;

public interface IUserService
{ 
    Task<UserResponseDto> CreateUserAsync(UserCreateDto input);
    Task DeleteUserAsync(Guid userId);
    Task<UserResponseDto> UpdateUserAsync(Guid userId, UserUpdateDto input);
    Task<UserResponseDto> GetUserByIdAsync(Guid userId);
    Task<UserResponseDto> GetUserByEmailAsync(string email);
    Task<List<UserResponseDto>> GetAllUsersAsync();
}