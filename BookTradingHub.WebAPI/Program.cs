using BookTradingHub.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using BookTradingHub.WebAPI.Services;
using BookTradingHub.WebAPI.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();  // Adds API controller services
builder.Services.AddEndpointsApiExplorer();  // Adds support for generating API docs
builder.Services.AddSwaggerGen(c =>

{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookTradingHub API", Version = "v1" });
});

// Add CORS policy to allow requests from your frontend (Blazor app).
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", builder =>
    {
        builder.WithOrigins("https://localhost:5169")  // Adjust to your Blazor app URL
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // Allow cookies and credentials if needed
    });
});



builder.Services.AddScoped<IAuthService, AuthService>();

// Register DbContext with SQLite
builder.Services.AddDbContext<ApplicationDB>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

AuthorizationPolicies.AddPolicies(builder.Services);

// Register Razor Components if needed
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger in development
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookTradingHub API v1");
        c.RoutePrefix = string.Empty;  // Swagger UI at the root of the API
    });
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDB>();
    DbSeeder.Seed(context);  // Call the seeding method
}

app.UseHttpsRedirection();  // Ensure HTTP requests are redirected to HTTPS

app.MapControllers();  // Map API controllers to routes

app.UseAuthentication();
app.UseAuthorization();


// Optional: Add static file support if needed
app.MapStaticAssets();

app.Run();

