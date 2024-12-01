using BookTradingHub.WebApp.Components;
using Blazored.LocalStorage;
<<<<<<< HEAD
using BookTradingHub.WebAPI.Auth;
using BookTradingHub.WebApp.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;
=======
using Syncfusion.Blazor;
>>>>>>> bc745eae5ac03e3115fd7b7b37dbf4ec9eb151d0

var builder = WebApplication.CreateBuilder(args);

// Add HttpClient with the base URL of your API
builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7167");  // Your WebAPI base URL
});

<<<<<<< HEAD
// Add scoped HttpClient for dependency injection
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7167") });
=======
// Register Syncfusion Blazor service
builder.Services.AddSyncfusionBlazor();

>>>>>>> bc745eae5ac03e3115fd7b7b37dbf4ec9eb151d0

builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/login";
});

builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);

// Add Razor Components and Blazored Local Storage
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Corrected usage of UseExceptionHandler
    app.UseHsts();
}

app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serves static assets like CSS, JS, etc.
app.UseAntiforgery();
app.MapStaticAssets(); // Maps the static assets
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Adds Razor Components

// Run the application
app.Run();
