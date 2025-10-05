using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using BlazorTodo.WebUI.Components;
using BlazorTodo.WebUI.Data;

var builder = WebApplication.CreateBuilder(args);

// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

// Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// HttpClient
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpClient>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var baseAddress = $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Database 
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
