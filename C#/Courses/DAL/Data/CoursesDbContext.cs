using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class CoursesDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Host=localhost;Port=5433;Database=courses-db;Username=courses-db;Password=courses-db";
        optionsBuilder.UseNpgsql(connectionString);
    }
}