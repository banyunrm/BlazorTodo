using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using BlazorTodo.WebUI.Components;
using BlazorTodo.WebUI.Data;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

// Tambahkan Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Tambahkan HttpClient (untuk Blazor)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpClient>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var baseAddress = $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});

// Tambahkan Controllers (untuk API)
builder.Services.AddControllers();

var app = builder.Build();

// Buat database jika belum ada
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
