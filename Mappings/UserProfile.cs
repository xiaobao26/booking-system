using AutoMapper;
using booking_system.Dtos;
using booking_system.Models;

namespace booking_system.Mappings;

public class UserProfile: Profile
{
    public UserProfile()
    {
        // Source -> Destination
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}