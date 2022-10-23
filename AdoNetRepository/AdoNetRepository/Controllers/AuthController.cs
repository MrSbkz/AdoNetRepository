using AdoNetRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdoNetRepository.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(string userName, string password)
    {
        try
        {
            await _authService.RegisterUserAsync(userName, password);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(string userName, string password)
    {
        try
        {
            return Ok(await _authService.LoginAsync(userName, password));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}