using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookTradingHub.Database.Data;
using BookTradingHub.WebAPI.Models;
using BCrypt.Net;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDB _context;

    public AuthController(ApplicationDB context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        if (_context.Users.Any(u => u.username == user.username))
            return BadRequest("Username already exists.");

        // Hash the password (simplified here; use a library like BCrypt)
        user.passwordhash = BCrypt.Net.BCrypt.HashPassword(user.passwordhash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDTO login)
    {
        var user = _context.Users.SingleOrDefault(u => u.username == login.username);
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "BookTradingHub",
            audience: "BookTradingHub",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
