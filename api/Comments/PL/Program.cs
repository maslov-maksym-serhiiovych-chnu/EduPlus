using BLL.Services;
using DAL.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CommentRepository>(_ => new CommentRepository(
    builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<CommentService>();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();