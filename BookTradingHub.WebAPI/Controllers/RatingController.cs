using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; // Namespace for ApplicationDB context
using BookTradingHub.WebAPI.Models; // Namespace for Book model
using System.Collections.Generic;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly ApplicationDB _context;

    public RatingsController(ApplicationDB context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddRating(Rating rating)
    {
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRating), new { id = rating.rating_id }, rating);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rating>> GetRating(int id)
    {
        var rating = await _context.Ratings
            .Include(r => r.user)
            .Include(r => r.book)
            .FirstOrDefaultAsync(r => r.rating_id == id);

        if (rating == null)
        {
            return NotFound();
        }

        return rating;
    }

    [HttpGet("book/{bookId}")]
    public async Task<ActionResult<IEnumerable<Rating>>> GetRatingsForBook(int bookId)
    {
        return await _context.Ratings
            .Where(r => r.book_id == bookId)
            .Include(r => r.user)
            .ToListAsync();
    }
}