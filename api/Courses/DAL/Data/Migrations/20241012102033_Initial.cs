using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Learn the basics of programming using Python.", "Introduction to Programming" },
                    { 2, "An in-depth study of data structures and algorithms.", "Data Structures and Algorithms" },
                    { 3, "Building dynamic websites using HTML, CSS, and JavaScript.", "Web Development" },
                    { 4, "Understanding the concepts of databases and SQL.", "Database Management Systems" },
                    { 5, "An introduction to machine learning concepts and techniques.", "Machine Learning" },
                    { 6, "Principles and practices of software engineering.", "Software Engineering" },
                    { 7, "Creating applications for iOS and Android platforms.", "Mobile App Development" },
                    { 8, "Basics of protecting systems and networks from cyber threats.", "Cybersecurity Fundamentals" },
                    { 9, "Exploring cloud services and deployment models.", "Cloud Computing" },
                    { 10, "Designing user-friendly interfaces and experiences.", "User Experience Design" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
