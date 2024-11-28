using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using BookTradingHub.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();  // Adds API controller services
builder.Services.AddEndpointsApiExplorer();  // Adds support for generating API docs
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookTradingHub API", Version = "v1" });
});

// Configure SQL Server with DbContext
builder.Services.AddDbContext<ApplicationDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Razor Components (not strictly necessary unless you're adding Blazor components in the API)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BookTradingHub",
            ValidAudience = "BookTradingHub",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Add this line to enable Swagger JSON generation
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookTradingHub API v1");
        c.RoutePrefix = string.Empty;  // Optional: Serve Swagger UI at the root of your API (http://localhost:7167/)
    });
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseAuthorization();  // Authorization middleware (if using authentication)

app.MapControllers();  // Map API controllers to routes

// Optional: Add support for antiforgery if needed
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

// Optional: Add static file support if needed (if you have static content like images or files)
app.MapStaticAssets();

app.Run();
