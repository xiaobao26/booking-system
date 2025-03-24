using booking_system.Contracts;
using booking_system.Dtos;
using booking_system.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking_system.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> CreateUserAsync(UserCreateDto input)
    {
        try
        {
            var result = await _userService.CreateUserAsync(input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add user failed.");
            throw;
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        try
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete user failed.");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<UserResponseDto>> UpdateUserAsync(Guid userId, UserUpdateDto input)
    {
        try
        {
            var result = await _userService.UpdateUserAsync(userId, input);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update user failed.");
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(Guid userId)
    {
        try
        {
            var result = await _userService.GetUserByIdAsync(userId);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
    {
        try
        {
            var result = await _userService.GetAllUsersAsync();
            return result;
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}