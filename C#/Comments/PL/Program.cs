using DAL.Data;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<NpgsqlDataSource>(_ =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    return NpgsqlDataSource.Create(connectionString);
});
builder.Services.AddTransient<CommentRepository>();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();