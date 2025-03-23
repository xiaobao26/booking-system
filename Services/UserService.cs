using AutoMapper;
using booking_system.Data;
using booking_system.Dtos;
using booking_system.Models;
using booking_system.Repositories;

namespace booking_system.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserResponseDto> CreateUserAsync(UserCreateDto input)
    {
        var user = _mapper.Map<User>(input);
        await _userRepository.AddUser(user);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    // public async Task<bool> DeleteUserAsync(Guid userId)
    // {
    //     
    // }
    // public async Task<UserUpdateDto> UpdateUserAsync(UserUpdateDto input)
    // {
    //     
    // }
    //
    // public async Task<List<UserResponseDto>> GetAllUsersAsync()
    // {
    //     
    // }
}