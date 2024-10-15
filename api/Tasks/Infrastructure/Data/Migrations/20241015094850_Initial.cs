using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Buy fruits, vegetables, and dairy products.", "Grocery Shopping" },
                    { 2, "Finish the quarterly financial report by Friday.", "Complete Report" },
                    { 3, "Take Max for a walk in the park.", "Walk the Dog" },
                    { 4, "Attend a yoga class at 6 PM.", "Gym Workout" },
                    { 5, "Read 'The Great Gatsby' for book club.", "Read Book" },
                    { 6, "Send updates to the team regarding project status.", "Email Project Updates" },
                    { 7, "Call to book a check-up appointment.", "Schedule Doctor Appointment" },
                    { 8, "Research destinations and book flights.", "Plan Vacation" },
                    { 9, "Tidy up the living room and kitchen.", "Clean the House" },
                    { 10, "Create slides for the upcoming meeting.", "Prepare Presentation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks");
        }
    }
}
