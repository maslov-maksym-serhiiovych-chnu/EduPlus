using DAL.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<NpgsqlDataSource>(_ =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    return NpgsqlDataSource.Create(connectionString);
});
builder.Services.AddControllers();
builder.Services.AddScoped<DapperCourseRepository>();
builder.Services.AddScoped<DapperCourseUserRepository>();

WebApplication app = builder.Build();
app.MapControllers();
app.Run();