using booking_system.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking_system.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var token = await _authService.Login(email, password);
        return Ok(token);
    }

    [HttpPost("login-with-refreshtoken")]
    public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
    {
        var result = await _authService.LoginWithRefreshToken(refreshToken);
        return Ok(result);
    }
}