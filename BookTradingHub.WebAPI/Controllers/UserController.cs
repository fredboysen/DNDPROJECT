using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; 
using BookTradingHub.WebAPI.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDB _context;

    public UserController(ApplicationDB context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
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
