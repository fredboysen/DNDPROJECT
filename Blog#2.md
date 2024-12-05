# 2 Design & Implementation
## Describe how you worked with your RESTFUL web API. Provide code examples.

Our project uses a RESTful API implemented with ASP.NET Core to manage books, ratings, and user authentication. The API adheres to REST conventions and standard HTTP methods, enabling seamless communication between the Blazor frontend and the backend. 

### 1. Books Management

This endpoint retrieves a list of all books from the database:
```csharp
 [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
```

### 2. Rating System
This endpoint allows users to add ratings for books, delete ratings if deemed not valuable and retrieve ratings from the database. 
```csharp
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
```

### 3. User Authentication
This endpoint handles user login and registration of account. 
```csharp
[HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            User user = await authService.UserValidation(loginDTO.username, loginDTO.password);
            string token = GenerateJwt(user);


            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null) return BadRequest("User details are required.");


        try
        {
            // Add user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User successfully registered.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error registering user: {ex.Message}");
        }
    }
```

## Give an overview of your web API endpoints.
The web API includes the following endpoints:

### Authentication
**POST/auth/login** - log in to your existing account<br>
**POST/auth/register** - register an account for using the application<br>

### Books 
**GET /api/books** - retrieves books from the database.<br>
**POST /api/books** - adds a book to the database.<br>
**DELETE /api/books** - deletes a book from the database.<br>

### Rating
**POST/api/Rating/AddRating** - add a rating for a book<br>
**DELETE/api/Rating/DeleteRating/{Id}** - delete an existing rating for a book<br>
**GET/api/Rating/GetRatings** - retrieve ratings for books<br>
<br>
<br>
<br>

The ApplicationDB.cs acts as our database context file, this defines how the database “BookTradingHub.db should interact with the application and vice versa.  Below is an example of the context class

**For saving books, users and rating:**

```csharp
namespace BookTradingHub.Database.Data
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
```
The context class maps the schema to the database, below are the models which act as the tables and fields in the database. 

**Book:**
```csharp
    public class Book
    {
        [Key] public int  book_Id { get; set; } required
        public string title { get; set; } = "";
        public string author { get; set; } = "";
        public string isbn { get; set; } = "";
        public string genre { get; set; } = "";
        public string publisher { get; set; } = "";
        public DateTime publish_Date { get; set; }
        public string condition { get; set; } = "";
        public double averageRating { get; set; } = 0.0;


        public string ImageUrl {get; set;} = "";
    }
```

**Rating:**
```csharp
  public class Rating
    {
        [Key]
        public int rating_id { get; set; }


        public int book_Id { get; set; }
        public Book? Book { get; set; }


        public string? title { get; set; }
        public int stars { get; set; }
        public string review { get; set; } = string.Empty;
    }
```

**User:**    
```csharp
public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }


        [Required]
        public string username { get; set; } = string.Empty;


        [Required]
        public string password { get; set; } = string.Empty;


        public string email { get; set; } = string.Empty;
        public string role { get; set; } = "User";
    }
```



Additionally we have a Database Seeder set up in order to populate the database at runtime with mock data. 
This is done in DBSeeder.cs
```csharp
 public static class DbSeeder
    {
        public static void Seed(ApplicationDB context)
        {
            context.Books.RemoveRange(context.Books);
            context.Users.RemoveRange(context.Users);
            context.Ratings.RemoveRange(context.Ratings);


            var books = new[]
            {
                new Book
                {
                    title = "Soul",
                    author = "Olivia Wilson",
                    isbn = "1234567890",
                    genre = "Fiction",
                    publisher = "Penguin Random House",
                    publish_Date = new DateTime(2020, 6, 1),
                    condition = "New",
                    averageRating = 4.5,
                    ImageUrl = "book1.jpg"
                },
                new Book
                {

            context.Books.AddRange(books);
            context.SaveChanges();

            var ratings = books.Select((book, index) => new Rating
            {
                book_Id = book.book_Id,
                title = book.title,
                stars = 3 + (index % 3),
                review = $"This is an amazing book by {book.author}."
            }).ToArray();

            context.Ratings.AddRange(ratings);
            context.SaveChanges();

            var users = new[]
            {
                new User
                {
                    username = "testuser",
                    email = "testuser@example.com",
                    role = "user",
                    password = "test123"
                },
                new User
                {
                    username = "testadmin",
                    email = "testadmin@example.com",
                    role = "admin",
                    password = "test123"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
```
Below the Array created this seeder interacts with the Db context. <br>

To initialize the data from the array into the tables constructed by the model class to the database. 
