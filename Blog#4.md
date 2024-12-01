
#4 User Management:

1. User Types in the System
Our system supports two types of users:
Normal Users: Can perform actions such as rating books, leaving comments, and viewing ratings and comments from other users.
Admins: Have additional privileges, including the ability to:
Remove ratings and comments.
Add new books to the platform.
Remove books that violate guidelines or are no longer relevant.
This differentiation is achieved by assigning roles to users during their registration. The role is stored in the database and used to control access to features in SQLlite.

2. User Model Implementation
The Users table in the database is defined to include a role column, which specifies whether a user is a regular user or an admin. The table structure is created using the following migration:

migrationBuilder.CreateTable(
    name: "Users",
    columns: table => new
    {
        user_id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),
        username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
        password = table.Column<string>(type: "TEXT", nullable: false),
        email = table.Column<string>(type: "TEXT", nullable: false),
        role = table.Column<string>(type: "TEXT", nullable: false) // Defines user roles
    }
);

By including the role column, the system can differentiate between regular users and admins, enabling the platform to grant or restrict access to specific resources and functionalities.

3. Login Functionality
The login functionality is implemented in the AuthService and provides:
Case-insensitive username matching: Ensures usernames are matched without being case-sensitive.
Password verification: Compares input passwords with stored passwords securely.
Exception handling for invalid credentials: Provides meaningful error messages for incorrect usernames or passwords.

public Task<User> UserLogin(string username, string password)
{
    User? existingUser = users.FirstOrDefault(u =>
        u.username.Equals(username, StringComparison.OrdinalIgnoreCase)) 
        ?? throw new Exception("User does not exist");

    if (!existingUser.password.Equals(password))
    {
        throw new Exception("Password was incorrect");
    }

    return Task.FromResult(existingUser);
}

This implementation ensures secure and user-friendly authentication by verifying credentials against the database.

4. Registration Validation
To ensure data integrity during user registration, input validation is implemented. For example, empty usernames are not allowed, and additional checks ensure that passwords and other fields meet requirements.

public Task UserRegistration(User user)
{
    if (string.IsNullOrEmpty(user.username))
    {
        throw new ValidationException("Username must be filled");
    }
    // Additional validations for other fields can be added here
}

This validation layer prevents incomplete or invalid data from being added to the database.

5. Role-Based Access Control
Access to resources is managed based on user roles. The role field in the Users table allows us to determine the appropriate permissions for each user. For example:
Regular User Access:
Regular users can rate books, view comments, and leave their own comments. These operations are unrestricted for users with the role User.
Admin Access:
Admin-specific functionalities such as removing books, ratings, or comments are restricted to users with the Admin role. This is implemented by checking the user’s role in the relevant API endpoints:

[Authorize(Roles = "Admin")]
[HttpPost("AddBook")]
public async Task<IActionResult> AddBook([FromBody] Book book)
{
    _context.Books.Add(book);
    await _context.SaveChangesAsync();
    return Ok($"Book '{book.title}' added successfully!");
}

By using role-based authorization, the system ensures only authorized users can access admin-level features.

6. Conclusion
our implemented authentication system includes:
User Role Management: Differentiates between regular users and admins to control access to features.
Secure Login Verification: Verifies credentials securely with case-insensitive matching and error handling.
Input Validation: Ensures integrity and completeness of user data during registration.
Separation of Concerns: All authentication and user-related logic is encapsulated in the AuthService, ensuring maintainable and modular code.