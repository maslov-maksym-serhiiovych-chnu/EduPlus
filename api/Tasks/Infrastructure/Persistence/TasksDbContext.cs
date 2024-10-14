using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TasksDbContext(DbContextOptions<TasksDbContext> options) : DbContext(options)
{
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskModel>().ToTable("tasks");

        modelBuilder.Entity<TaskModel>().HasKey(c => c.Id);

        modelBuilder.Entity<TaskModel>()
            .Property(c => c.Id)
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();

        modelBuilder.Entity<TaskModel>()
            .Property(c => c.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        modelBuilder.Entity<TaskModel>()
            .Property(c => c.Description)
            .HasColumnName("description")
            .HasColumnType("TEXT");

        modelBuilder.Entity<TaskModel>().HasData(
            new TaskModel
            {
                Id = 1, Name = "Grocery Shopping", Description = "Buy fruits, vegetables, and dairy products."
            },
            new TaskModel
            {
                Id = 2, Name = "Complete Report", Description = "Finish the quarterly financial report by Friday."
            },
            new TaskModel
            {
                Id = 3, Name = "Walk the Dog", Description = "Take Max for a walk in the park."
            },
            new TaskModel
            {
                Id = 4, Name = "Gym Workout", Description = "Attend a yoga class at 6 PM."
            },
            new TaskModel
            {
                Id = 5, Name = "Read Book", Description = "Read 'The Great Gatsby' for book club."
            },
            new TaskModel
            {
                Id = 6, Name = "Email Project Updates",
                Description = "Send updates to the team regarding project status."
            },
            new TaskModel
            {
                Id = 7, Name = "Schedule Doctor Appointment", Description = "Call to book a check-up appointment."
            },
            new TaskModel
            {
                Id = 8, Name = "Plan Vacation", Description = "Research destinations and book flights."
            },
            new TaskModel
            {
                Id = 9, Name = "Clean the House", Description = "Tidy up the living room and kitchen."
            },
            new TaskModel
            {
                Id = 10, Name = "Prepare Presentation", Description = "Create slides for the upcoming meeting."
            }
        );
    }
}