using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class CoursesDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>().ToTable("courses");
        modelBuilder.Entity<Course>().HasKey(c => c.Id);

        modelBuilder.Entity<Course>()
            .Property(c => c.Id)
            .HasColumnName("id")
            .HasColumnType("serial")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Course>()
            .Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Course>()
            .Property(c => c.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5433;Database=courses-db;Username=courses-db;Password=courses-db"
        );
    }
}