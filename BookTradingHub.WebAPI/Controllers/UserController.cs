using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data;
using BookTradingHub.WebAPI.Models;

namespace BookTradingHub.WebAPI.Controllers
{
   [ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ApplicationDB _context;

    public UserController(ApplicationDB context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        // Check for a valid user in the database based on username and password
        var validatedUser = _context.Users
            .FirstOrDefault(u => u.username == loginDto.username && u.password == loginDto.password);

        if (validatedUser != null)
        {
            // Successfully authenticated
            return Ok(new { message = "Login successful" });
        }
        else
        {
            // Invalid credentials
            return Unauthorized(new { message = "Invalid username or password" });
        }
    }

    [HttpPost("register")]
public async Task<IActionResult> Register(User user)
{
    if (user == null || string.IsNullOrEmpty(user.username) || string.IsNullOrEmpty(user.password))
    {
        return BadRequest("Invalid user data.");
    }
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
    

    [HttpGet]
[Route("api/user")]
public IActionResult GetUserFromToken()
{
    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "user_Id");
    if (userIdClaim == null)
        return Unauthorized("User ID not found in token");

    var userId = int.Parse(userIdClaim.Value);
    var user = _context.Users.Find(userId);

    if (user == null)
        return NotFound("User not found");

    return Ok(new { user.user_id, user.username });
}

}
}