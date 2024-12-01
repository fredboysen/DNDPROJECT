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
    // Validate rating value
    if (rating.stars < 1 || rating.stars > 5)
        return BadRequest("Rating must be between 1 and 5");

    // Check if the book exists
    var book = await _context.Books.FindAsync(rating.book_Id);
    if (book == null)
        return NotFound("Book not found");

    // Set the title of the rating from the book title
    rating.title = book.title;  // Populate the title field

    // Add the rating to the database
    _context.Ratings.Add(rating);
    await _context.SaveChangesAsync();

    // Optionally update the book's average rating
    var ratingsForBook = _context.Ratings.Where(r => r.book_Id == book.book_Id);
    if (ratingsForBook.Any())
    {
        // Recalculate the average rating for the book
        book.averageRating = ratingsForBook.Average(r => r.stars);
    }
    else
    {
        book.averageRating = 0; // No ratings yet, set default to 0
    }

    // Save the updated average rating for the book
    await _context.SaveChangesAsync();

    return Ok($"Rating for '{book.title}' added successfully!");
}

 private async Task UpdateAverageRating(int bookId)
{
    // Fetch all ratings for the given book
    var ratingsForBook = await _context.Ratings
        .Where(r => r.book_Id == bookId)
        .ToListAsync();  // ToListAsync requires Microsoft.EntityFrameworkCore

    // Recalculate the average rating
    if (ratingsForBook.Any())
    {
        // Calculate the average and round to 2 decimal places
        var average = ratingsForBook.Average(r => r.stars);
        var roundedAverage = Math.Round(average, 2);  // Round to 2 decimal places

        var book = await _context.Books.FindAsync(bookId);
        if (book != null)
        {
            book.averageRating = roundedAverage;  // Update the average rating in the book table
            await _context.SaveChangesAsync();
        }
    }
    else
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book != null)
        {
            book.averageRating = 0.0;  // No ratings yet, set default to 0
            await _context.SaveChangesAsync();
        }
    }
}       
[HttpGet("GetRatingsForBook/{bookId}")]
public async Task<IActionResult> GetRatingsForBook(int bookId)
{
    // Fetch all ratings for the given book
    var ratings = await _context.Ratings
        .Where(r => r.book_Id == bookId)
        .ToListAsync();
    
    if (ratings == null || ratings.Count == 0)
    {
        return NotFound("No ratings found for this book.");
    }

    return Ok(ratings);
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
            .Include(r => r.Book)  // Make sure the Book details are included
            .ToListAsync();

        return Ok(ratings);
    }
    catch (Exception ex)
    {
        // Log the exception for debugging purposes
        Console.WriteLine($"Error fetching ratings: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}
    }
}

 








