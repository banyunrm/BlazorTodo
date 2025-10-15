using Microsoft.EntityFrameworkCore;
using BlazorTodo.WebUI2.Models;

namespace BlazorTodo.WebUI2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Country> Countries { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todos");
            modelBuilder.Entity<Country>().ToTable("Countries"); 
        }
    }
}