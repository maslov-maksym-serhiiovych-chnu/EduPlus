using DAL.Data.ADO.NET;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<CourseRepository>(_ =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    NpgsqlConnection connection = new(connectionString);
    return new CourseRepository(connection);
});

WebApplication app = builder.Build();
app.MapControllers();
app.Run();