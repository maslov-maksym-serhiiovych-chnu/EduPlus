﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(TasksDbContext))]
    partial class TasksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tasks", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Buy fruits, vegetables, and dairy products.",
                            Name = "Grocery Shopping"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Finish the quarterly financial report by Friday.",
                            Name = "Complete Report"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Take Max for a walk in the park.",
                            Name = "Walk the Dog"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Attend a yoga class at 6 PM.",
                            Name = "Gym Workout"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Read 'The Great Gatsby' for book club.",
                            Name = "Read Book"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Send updates to the team regarding project status.",
                            Name = "Email Project Updates"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Call to book a check-up appointment.",
                            Name = "Schedule Doctor Appointment"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Research destinations and book flights.",
                            Name = "Plan Vacation"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Tidy up the living room and kitchen.",
                            Name = "Clean the House"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Create slides for the upcoming meeting.",
                            Name = "Prepare Presentation"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
