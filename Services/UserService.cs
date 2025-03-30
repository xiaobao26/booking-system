using AutoMapper;
using booking_system.Contracts;
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
        _userRepository.AddUser(user);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var targetUser = await _userRepository.FindUserByIdAsync(userId);
        if (targetUser == null) throw new NotFoundException("User cannot found");
        
        _userRepository.DeleteUser(targetUser);
        await _userRepository.SaveChangesAsync();
        
    }
    public async Task<UserResponseDto> UpdateUserAsync(Guid userId, UserUpdateDto input)
    {
        var targetUser = await _userRepository.FindUserByIdAsync(userId);
        if (targetUser == null) throw new NotFoundException("User cannot found");
        
        _userRepository.UpdateUser(targetUser, input);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(targetUser);
    }

    public async Task<UserResponseDto> GetUserByIdAsync(Guid userId)
    {
        var targetUser = await _userRepository.FindUserByIdAsync(userId);
        if (targetUser == null) throw new NotFoundException("User cannot found");
        
        return _mapper.Map<UserResponseDto>(targetUser);
    }
    
    public async Task<UserResponseDto> GetUserByEmailAsync(string email)
    {
        var targetUser = await _userRepository.FindUserByEmailAsync(email);
        if (targetUser == null) throw new NotFoundException("User cannot found");

        return _mapper.Map<UserResponseDto>(targetUser);
    }
    
    public async Task<List<UserResponseDto>> GetAllUsersAsync()
    {
        var allUsers = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<List<UserResponseDto>>(allUsers);
    }
}