using booking_system.Dtos;

namespace booking_system.Services;

public interface IUserService
{ 
    Task<UserResponseDto> CreateUserAsync(UserCreateDto input);
    // Task<bool> DeleteUserAsync(Guid userId);
    // Task<UserUpdateDto> UpdateUserAsync(UserUpdateDto input);
    // Task<List<UserResponseDto>> GetAllUsersAsync();
}