using BookTradingHub.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 


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
        builder.WithOrigins("https://localhost:5000")  // Adjust to your Blazor app URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register DbContext with SQLite
builder.Services.AddDbContext<ApplicationDB>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

// Apply CORS policy globally
app.UseCors("AllowBlazorApp");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDB>();
    DbSeeder.Seed(context);  // Call the seeding method
}

app.UseHttpsRedirection();  // Ensure HTTP requests are redirected to HTTPS

app.MapControllers();  // Map API controllers to routes

// Optional: Add static file support if needed
app.MapStaticAssets();

app.Run();

