using BookTradingHub.WebApp.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Blazored.LocalStorage;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add HttpClient with the base URL of your API
builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7167");  // Your WebAPI base URL
});

// Register Syncfusion Blazor service
builder.Services.AddSyncfusionBlazor();



builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();


var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
