using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("courses");

        modelBuilder.Entity<Course>().HasKey(c => c.Id);

        modelBuilder.Entity<Course>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Course>()
            .Property(c => c.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        modelBuilder.Entity<Course>()
            .Property(c => c.Description)
            .HasColumnName("description")
            .HasColumnType("TEXT");
    }
}