using DAL.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<AdoDotNetCourseRepository>(_ =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    NpgsqlConnection connection = new(connectionString);
    return new AdoDotNetCourseRepository(connection);
});

WebApplication app = builder.Build();
app.MapControllers();
app.Run();