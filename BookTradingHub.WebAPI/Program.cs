using BookTradingHub.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using BookTradingHub.WebAPI.Services;
using BookTradingHub.WebAPI.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();  
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>

{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookTradingHub API", Version = "v1" });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", builder =>
    {
        builder.WithOrigins("https://localhost:5169")  
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); 
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


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookTradingHub API v1");
        c.RoutePrefix = string.Empty;  
    });
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials());

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDB>();
    DbSeeder.Seed(context);  
}

app.UseHttpsRedirection();  

app.MapControllers();  

app.UseAuthentication();
app.UseAuthorization();



app.MapStaticAssets();

app.Run();

