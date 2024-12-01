using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; // Namespace for ApplicationDB context
using BookTradingHub.WebAPI.Models; // Namespace for Book model
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookTradingHub.WebAPI.Services;


namespace BookTradingHub.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;
private readonly ApplicationDB _context;

    public AuthController(IConfiguration config, IAuthService authService, ApplicationDB context)
    {
        this.config = config;
        this.authService = authService;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            User user = await authService.UserValidation(loginDTO.username, loginDTO.password);
            string token = GenerateJwt(user);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null) return BadRequest("User details are required.");

        try
        {
            // Add user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User successfully registered.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error registering user: {ex.Message}");
        }
    }

    private string GenerateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");

        List<Claim> claims = GenerateClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> GenerateClaims(User user)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim(ClaimTypes.Name, user.username),
        new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
        new Claim(ClaimTypes.Role, user.role), // Use ClaimTypes.Role for consistency.
    };
    return claims.ToList();
}

    

    
}


