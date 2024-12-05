# 3 Web Application
## Describe how your key requirements are implemented in your web application. Provide code examples.

### Key Requirements Implementation
Our web application implements several core requirements through a combination of frontend and backend components:


**User Authentication System:**
- The system uses role-based authentication to differentiate between regular users and administrators
- Login functionality includes case-insensitive username matching and secure password verification
- User data is stored in SQLite database with proper validation checks

**Book Management System:**
- Books are stored with information including title, author, genre, and average rating
- The database schema supports relationships between books, ratings, and users
- Images are handled through URL storage for efficient management

**Rating System:**
- Users can submit ratings (1-5) for books
- The system automatically calculates and updates average ratings
- Input validation ensures rating values are within acceptable ranges
- Each rating is associated with both a user and a book through foreign key relationships

## Give an overview of the pages in your web application.

**Home/Landing Page:**
- Displays featured books and average ratings
- Provides navigation to other sections of the application

**Book Listing Page:**
- Shows all available books with basic information
- Implements filtering and sorting capabilities
- Displays average ratings for each book

**Add Rating Page:**
- Allows users to select a book to rate
- Shows “hot to cold” spectre for visual rating
- Numerical rating of 1 to 5

**Add Book Page:**
-Allows users to add a book for all to rate
-Posts a new book to db and set average rating to 0 (until someone rates it)

**View Ratings Page:**
- Allows admins to view existing ratings
- Possibility to delete ratings if deemed spam/irrelevant

**Manage Books Page:**
-Allows admins to delete a book added to the db by a user
-Overview of all books in the system

**Login/Register Page:**
- Allows users to login and non-users to register an account


## How key features are implemented

**Book Data Retrieval:**
```csharp
[HttpGet]
public async Task<ActionResult<List<Book>>> GetBooks()
{
    var books = await _context.Books.ToListAsync();
    return Ok(books);
}
```


**Rating Submission:**
```csharp
[HttpPost("AddRating")]
public async Task<IActionResult> AddRating([FromBody] Rating rating)
{
    if (rating.stars < 1 || rating.stars > 10)
        return BadRequest("Rating must be between 1 and 10");

    var book = await _context.Books.FindAsync(rating.book_id);
    if (book == null)
        return NotFound("Book not found");

    _context.Ratings.Add(rating);
    await _context.SaveChangesAsync();
}
```


**User Authentication:**
*Login task*
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
```
*User validation to DB*
```csharp
{
users = context.Users.ToList();
}


private readonly IList<User> users;




public Task<User> UserValidation(string username, string password)
{
    User? existingUser = users.FirstOrDefault(u =>
   
    u.username.Equals(username,StringComparison.OrdinalIgnoreCase)) ?? throw new Exception("User does not exist");


        if (!existingUser.password.Equals(password))
        {
            throw new Exception ("Password was incorrect");
        }


    return Task.FromResult(existingUser);
}
```   

**authService.UserValidation:** Validates the user credentials by checking against stored users.<br>
**GenerateJwt(user):** Generates a JWT (JSON Web Token) for the authenticated user.<br><br>
If validation is successful, the method returns Ok(token), sending the token to the client.
If an exception is thrown (e.g., user not found, incorrect password), it returns a BadRequest with the error message.


## Describe how your frontend connects to your web service. Provide Code Examples.

**Backend launch settings:**
```json
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5140",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7167",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```


**HTTP Profile:**<br>
Runs on http://localhost:5140 without launching a browser.<br>
**HTTPS Profile:**<br>
Runs on https://localhost:7167 with browser launch enabled.<br><br>
*The backend uses the HTTPS endpoint (https://localhost:7167) as the primary URL for serving API requests during development.*

**Frontend:**
API calls to backend 

home.cs fetching data from API:
```csharp
  private List<Book> Books = new List<Book>();
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Books = await Http.GetFromJsonAsync<List<Book>>("https://localhost:7167/api/books");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching books: {ex.Message}");
        }
    }
```

An API call is made to the endpoint https://localhost:7167/api/books using Http.GetFromJsonAsync<List<Book>>. <br>
The response is deserialized into a list of Book objects and stored in the Books variable.


Program.cs (WEBAPP) -- Adding http
```csharp
builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7167");  
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7167") });
```

The frontend (web application) adds an HTTP Client for making API calls to the backend:
- The base address for the HTTP client is set to https://localhost:7167 (the backend's HTTPS URL).
- This is done using the AddHttpClient service in Program.cs, which ensures the frontend knows where to send request

### Flow: 
**Frontend:**
A Blazor WebAssembly or Razor component (e.g., Home.cs) initializes and needs to fetch data.
It makes an HTTP GET request to the backend API (e.g., https://localhost:7167/api/books).

**Backend:**
The backend listens for API calls on the configured https://localhost:7167 endpoint.
When a request comes in, the backend processes it and sends a JSON response.
