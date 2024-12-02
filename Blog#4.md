# 4 User Management
## Describe what users exist in your system and how you have implemented log-in functionality. Provide code examples.

The system can support different user types, such as:
- **Regular Users:** set as default at sign up
- **Admins:** Users with administrative rights to manage resources, users, or policies. (must be manually added to the database)

### Login Functionality Implementation
The login functionality is implemented using JWT-based authentication, where the user provides credentials and receives a token if they are valid. Below is a typical implementation:<br><br>
**Example of Login Endpoint:**
```csharp
[ApiController]
[Route("[controller]")]


public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;
private readonly ApplicationDB _context;


    public AuthController(IConfiguration config, IAuthService authService, ApplicationDB context)
    {
        this.config = config;
        this.authService = authService;
        _context = context;
    }


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
Authentication Logic in AuthService:
```csharp
public class AuthService : IAuthService
{
    public AuthService(ApplicationDB context)
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

Token generation:
```csharp
private string GenerateJwt(User user)
{
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");


        List<Claim> claims = GenerateClaims(user);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };


        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
```



Access to Resources
Access to resources is managed using role-based authorization. Each user is assigned a role (e.g., "Admin", "User"), and their role determines which actions or resources they can access.
Policies define access rules for specific actions or resources:

```csharp
public static class AuthorizationPolicies
    {
        public static void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("User", a =>
                    a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "User"));
                options.AddPolicy("Admin", a =>
                    a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "Admin"));
            });
        }
    }
}

```

### Code Example of Token Validation Middleware
Tokens are validated automatically by the middleware, using the AddAuthentication and AddJwtBearer configuration in Program.cs:
```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
        ClockSkew = TimeSpan.Zero,
    };
});

```
### Summary
Users exist in the system with roles like "User" or "Admin."
Login functionality is implemented through an AuthController that authenticates users and generates JWT tokens.
Access to resources is managed using role-based policies, enforced through attributes like <br>[Authorize(Policy = "RoleName")].
