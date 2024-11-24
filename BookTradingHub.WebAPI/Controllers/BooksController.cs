using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; // Namespace for ApplicationDB context
using BookTradingHub.WebAPI.Models; // Namespace for Book model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTradingHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDB _context;

        // Constructor to inject the DbContext
        public BooksController(ApplicationDB context)
        {
            _context = context;
        }

        // GET: api/books (this is the endpoint Blazor calls)
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            // The query to fetch all books from the Books table
            var books = await _context.Books.ToListAsync(); // This is the equivalent of "SELECT * FROM Books"
            return Ok(books); // Return the books as JSON response
        }
    }
}
