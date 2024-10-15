using BLL.Services;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using PL.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CoursesDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddScoped<CourseService>();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();
app.Run();