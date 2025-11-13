using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Refit;
using Web;
using Web.Data;

var builder = WebApplication.CreateBuilder(args);

// ===== Services =====
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// WeatherForecastService نمونه است
builder.Services.AddSingleton<WeatherForecastService>();

// TokenProvider با ProtectedSessionStorage
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<ProtectedSessionStorage>();

// AuthHeaderHandler برای افزودن Authorization header به request های محافظت‌شده
builder.Services.AddTransient<AuthHeaderHandler>();


// HttpContextAccessor در صورت نیاز به session/identity
builder.Services.AddHttpContextAccessor();


var apiBaseUrl = builder.Configuration["Api:BaseUrl"];

builder.Services.AddRefitClient<IHasibApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthHeaderHandler>();

// ===== HttpClient ها =====

// 1️⃣ HttpClient برای request های محافظت‌شده (با AuthHeaderHandler)
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7143"); // آدرس API
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // فقط Dev
    };
})
.AddHttpMessageHandler<AuthHeaderHandler>();

// 2️⃣ HttpClient برای login (بدون AuthHeaderHandler)
builder.Services.AddHttpClient("ApiClientNoAuth", client =>
{
    client.BaseAddress = new Uri("https://localhost:7143"); // همان آدرس API
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };
});

// Default HttpClient inject شده به کامپوننت‌ها همان ApiClient محافظت‌شده باشد
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("ApiClient");
});

// ===== Build App =====
var app = builder.Build();

// ===== Middleware =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Blazor Server endpoints
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
