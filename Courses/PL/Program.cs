using DAL.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
NpgsqlConnection connection = new(connectionString);
builder.Services.AddScoped<AdoDotNetCourseRepository>(_ => new AdoDotNetCourseRepository(connection));
builder.Services.AddScoped<AdoDotNetCourseUserRepository>(_ => new AdoDotNetCourseUserRepository(connection));
builder.Services.AddScoped<DapperCourseRepository>(_ => new DapperCourseRepository(connection));
builder.Services.AddScoped<DapperCourseUserRepository>(_ => new DapperCourseUserRepository(connection));

WebApplication app = builder.Build();
app.MapControllers();
app.Run();