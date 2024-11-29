using Microsoft.Extensions.Options; // For IOptions<T> injection
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookTradingHub.Database.Data;
using BookTradingHub.WebAPI.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDB _context;
    private readonly JwtSettings _jwtSettings; // Injected JwtSettings

    public AuthController(ApplicationDB context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value; // Access the JwtSettings values
    }

    // Register User (Asynchronous)
    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        if (await _context.Users.AnyAsync(u => u.username == user.username))
            return BadRequest("Username already exists.");

        // Hash the password (simplified here; use a library like BCrypt)
        user.passwordhash = BCrypt.Net.BCrypt.HashPassword(user.passwordhash);
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }

    // Login User (Asynchronous)
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        var user = await _context.Users
                                  .SingleOrDefaultAsync(u => u.username == login.username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(login.password, user.passwordhash))
            return Unauthorized("Invalid username or password.");

        var token = GenerateJwtToken(user);

        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.Role, user.role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtSettings.ExpiryInHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
