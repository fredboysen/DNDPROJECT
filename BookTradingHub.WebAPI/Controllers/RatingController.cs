using Microsoft.AspNetCore.Mvc;
using BookTradingHub.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data;

namespace BookTradingHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDB _context;

        public RatingController(ApplicationDB context)
        {
            _context = context;
        }
[HttpPost("AddRating")]
public async Task<IActionResult> AddRating([FromBody] Rating rating)
{
    
    if (rating.stars < 1 || rating.stars > 5)
        return BadRequest("Rating must be between 1 and 5");

    
    var book = await _context.Books.FindAsync(rating.book_Id);
    if (book == null)
        return NotFound("Book not found");

   
    rating.title = book.title;  

    
    _context.Ratings.Add(rating);
    await _context.SaveChangesAsync();

    
    var ratingsForBook = _context.Ratings.Where(r => r.book_Id == book.book_Id);
    if (ratingsForBook.Any())
    {
        
        book.averageRating = ratingsForBook.Average(r => r.stars);
    }
    else
    {
        book.averageRating = 0;
    }

    
    await _context.SaveChangesAsync();

    return Ok($"Rating for '{book.title}' added successfully!");
}

 private async Task UpdateAverageRating(int bookId)
{
    
    var ratingsForBook = await _context.Ratings
        .Where(r => r.book_Id == bookId)
        .ToListAsync();  

    
    if (ratingsForBook.Any())
    {
        
        var average = ratingsForBook.Average(r => r.stars);
        var roundedAverage = Math.Round(average, 2); 

        var book = await _context.Books.FindAsync(bookId);
        if (book != null)
        {
            book.averageRating = roundedAverage;  
            await _context.SaveChangesAsync();
        }
    }
    else
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book != null)
        {
            book.averageRating = 0.0; 
            await _context.SaveChangesAsync();
        }
    }
}       

  [HttpDelete("DeleteRating/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound("Rating not found");
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return Ok("Rating deleted successfully");
        }

       [HttpGet("GetRatings")]
public async Task<ActionResult<List<Rating>>> GetRatings()
{
    try
    {
        var ratings = await _context.Ratings
            .Include(r => r.Book) 
            .ToListAsync();

        return Ok(ratings);
    }
    catch (Exception ex)
    {
       
        Console.WriteLine($"Error fetching ratings: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}
    }
}

 








