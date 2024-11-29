using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; 
using BookTradingHub.WebAPI.Models; 
using System.Threading.Tasks;
using BCrypt.Net; // Add this for password hashing

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDB _context;

    public UserController(ApplicationDB context)
    {
        _context = context;
    }

    [HttpPost("register")] // Ensure this is the correct route for registration
  public async Task<IActionResult> CreateUser(User user)
{
    // Check if the username already exists
    var existingUser = await _context.Users
        .FirstOrDefaultAsync(u => u.username == user.username);

    if (existingUser != null)
    {
        return Conflict(new { message = "Username already exists." }); // Return 409 Conflict if the username exists
    }

    // Hash the password before storing it
    user.passwordhash = BCrypt.Net.BCrypt.HashPassword(user.passwordhash);

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.user_id }, user);
}
    

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }
}
