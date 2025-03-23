using booking_system.Dtos;
using booking_system.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking_system.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(UserCreateDto input)
    {
        var result = await _userService.CreateUserAsync(input);
        return Ok("Create user successful.");
    }
}