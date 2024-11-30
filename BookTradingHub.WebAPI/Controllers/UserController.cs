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

        // Login endpoint: checks only username and password
        [HttpPost]
public IActionResult Login([FromBody] User user)
{
    // Log the incoming request
    Console.WriteLine($"Received username: {user.username}, password: {user.password}");

    // Check if user exists in the database
    var validatedUser = _context.Users
    .FirstOrDefault(u => u.username.Trim().ToLower() == user.username.Trim().ToLower() &&
                         u.password == user.password);  // You could apply a similar trim and lower logic for the password if needed


    if (validatedUser != null)
    {
        // Successfully authenticated
        return Ok(new { message = "Login successful" });
    }
    else
    {
        // Log the error for invalid credentials
        Console.WriteLine($"Invalid credentials for username: {user.username}");

        // Invalid credentials
        return Unauthorized(new { message = "Invalid username or password" });
    }
}

    }}

