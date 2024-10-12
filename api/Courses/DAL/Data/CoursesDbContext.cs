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
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();

        modelBuilder.Entity<Course>()
            .Property(c => c.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        modelBuilder.Entity<Course>()
            .Property(c => c.Description)
            .HasColumnName("description")
            .HasColumnType("TEXT");

        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1, Name = "Introduction to Programming",
                Description = "Learn the basics of programming using Python."
            },
            new Course
            {
                Id = 2, Name = "Data Structures and Algorithms",
                Description = "An in-depth study of data structures and algorithms."
            },
            new Course
            {
                Id = 3, Name = "Web Development",
                Description = "Building dynamic websites using HTML, CSS, and JavaScript."
            },
            new Course
            {
                Id = 4, Name = "Database Management Systems",
                Description = "Understanding the concepts of databases and SQL."
            },
            new Course
            {
                Id = 5, Name = "Machine Learning",
                Description = "An introduction to machine learning concepts and techniques."
            },
            new Course
            {
                Id = 6, Name = "Software Engineering",
                Description = "Principles and practices of software engineering."
            },
            new Course
            {
                Id = 7, Name = "Mobile App Development",
                Description = "Creating applications for iOS and Android platforms."
            },
            new Course
            {
                Id = 8, Name = "Cybersecurity Fundamentals",
                Description = "Basics of protecting systems and networks from cyber threats."
            },
            new Course
            {
                Id = 9, Name = "Cloud Computing", Description = "Exploring cloud services and deployment models."
            },
            new Course
            {
                Id = 10, Name = "User Experience Design",
                Description = "Designing user-friendly interfaces and experiences."
            }
        );
    }
}