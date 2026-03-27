using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantServiceAPI.Application.Common;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;

namespace RestaurantServiceAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);

        if (result is null)
            return Unauthorized("Invalid login or password.");

        return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Login succesfully"));
    }
}