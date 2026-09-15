using checkBack.Common;
using checkBack.DTOs.Requests;
using checkBack.DTOs.Responses;
using checkBack.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace checkBack.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        var result = _authService.Authenticate(request.Username, request.Password);

        if (result is null)
            return Unauthorized(ApiResponse<object>.Fail("Invalid username or password"));

        return Ok(ApiResponse<LoginResponseDto>.Ok(new LoginResponseDto
        {
            Token = result.Token,
            Username = result.Username,
            Role = result.Role
        }));
    }
}
