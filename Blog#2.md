#2 Design & Implementation
Describe how you worked with your RESTFUL web API. Provide code examples.


Our project uses a RESTful API implemented with ASP.NET Core to manage books, ratings, and user authentication. The API adheres to REST conventions and standard HTTP methods, enabling seamless communication between the Blazor frontend and the backend.


1. Books Management

This endpoint retrieves a list of all books from the database:
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
    }

2. Rating System
This endpoint allows users to add ratings for books, validates inputs, and updates the book’s average rating dynamically:
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
This endpoint ensures data integrity by validating the input and recalculating the average rating of a book after a new rating is added.

3. User Authentication
This endpoint handles user login and validates credentials
public IActionResult Login([FromBody] User user)
{
    // Log the incoming request
    Console.WriteLine($"Received username: {user.username}, password: {user.password}");


    // Check if user exists in the database
    var validatedUser = _context.Users
    .FirstOrDefault(u => u.username.Trim().ToLower() == user.username.Trim().ToLower() &&
                         u.password == user.password);




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
This endpoint provides basic authentication functionality by checking the user's credentials against the database.

Give an overview of your web API endpoints.
The web API includes the following endpoints:

Books Management

GET /api/books - Retrieves all books from the database.
POST /api/books - Allows admins to add a new book (not shown in the example).
DELETE /api/books/{id} - Allows admins to delete a specific book.
Ratings Management

POST /api/ratings/AddRating - Enables users to submit a rating for a book and updates the book's average rating.
User Authentication

POST /api/users/Login - Authenticates a user and returns a success or failure message.

Describe how you are currently using file storage to store data. Provide code examples.

?????? 