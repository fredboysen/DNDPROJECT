using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; 
using BookTradingHub.WebAPI.Models;


namespace BookTradingHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDB _context;

        public BooksController(ApplicationDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync(); 
            return Ok(books);
        }

       [HttpPost]
public async Task<ActionResult<Book>> AddBook(Book book)
{
    if (book == null || string.IsNullOrWhiteSpace(book.ImageUrl))
    {
        return BadRequest("Invalid book data. ImageUrl is required.");
    }

    try
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBooks), new { id = book.book_Id }, book);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}


[HttpDelete("{id}")]
public async Task<IActionResult> DeleteBook(int id)
{
    var book = await _context.Books.FindAsync(id);
    if (book == null)
    {
        return NotFound();
    }

    _context.Books.Remove(book);
    await _context.SaveChangesAsync();

    return NoContent();
}

    }
}
