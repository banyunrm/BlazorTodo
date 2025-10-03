using Microsoft.EntityFrameworkCore;
using BlazorTodoApp.Models;

namespace BlazorTodoApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<Country> Countries { get; set; }
}
