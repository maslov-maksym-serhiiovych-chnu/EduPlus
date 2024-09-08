using DAL.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<CourseRepositoryTemp>(_ =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    NpgsqlConnection connection = new(connectionString);
    return new CourseRepositoryTemp(connection);
});

WebApplication app = builder.Build();
app.MapControllers();
app.Run();