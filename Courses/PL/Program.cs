using DAL.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<TempCourseRepository>(_ =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    NpgsqlConnection connection = new(connectionString);
    return new TempCourseRepository(connection);
});

WebApplication app = builder.Build();
app.MapControllers();
app.Run();