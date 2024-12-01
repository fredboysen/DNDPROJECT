using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.Database.Data; 
using BookTradingHub.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
