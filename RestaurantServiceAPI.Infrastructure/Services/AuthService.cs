using Microsoft.EntityFrameworkCore;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Application.Interfaces;
using RestaurantServiceAPI.Infrastructure.Data;

namespace RestaurantServiceAPI.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly RestaurantServiceDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        RestaurantServiceDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var account = await _context.PanelAccounts
            .FirstOrDefaultAsync(x => x.Login == request.Login);

        if (account is null || !account.IsActive)
            return null;

        var isPasswordValid = _passwordHasher.Verify(request.Password, account.PasswordHash);

        if (!isPasswordValid)
            return null;

        return _jwtTokenService.CreateToken(account);
    }
}