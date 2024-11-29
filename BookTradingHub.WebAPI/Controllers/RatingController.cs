using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.WebAPI.Models;
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

        // POST: api/Rating/AddRating
        [HttpPost("AddRating")]
        public async Task<IActionResult> AddRating([FromBody] Rating rating)
        {
            // Validate rating value
            if (rating.stars < 1 || rating.stars > 10)
                return BadRequest("Rating must be between 1 and 10");

            // Check if user exists
            var user = await _context.Users.FindAsync(rating.user_Id);
            if (user == null)
                return NotFound("User not found");

            // Check if book exists
            var book = await _context.Books.FindAsync(rating.book_id);
            if (book == null)
                return NotFound("Book not found");

            // Add the rating to the database
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            // Optionally update the book's average rating
            var ratingsForBook = _context.Ratings.Where(r => r.book_id == book.book_Id);
            if (ratingsForBook.Any())
            {
                // Recalculate average rating for the book
                book.averageRating = ratingsForBook.Average(r => r.stars);
            }
            else
            {
                book.averageRating = 0; // No ratings yet, set default to 0 or another default value
            }

            // Save the updated average rating for the book
            await _context.SaveChangesAsync();

            return Ok($"Rating for '{book.title}' added successfully!");
        }

        // GET: api/Rating/{bookId}
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetRatingsByBookId(int bookId)
        {
            var ratings = await _context.Ratings
                                        .Where(r => r.book_id == bookId)
                                        .Include(r => r.user)  // Optionally include user details
                                        .ToListAsync();

            if (!ratings.Any())
                return NotFound("No ratings found for this book.");

            return Ok(ratings);
        }
    }
}
