using System.Net;
using System.Text;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Npgsql;
using Testcontainers.PostgreSql;

namespace PL.Tests;

public class CoursesControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private const int ReadId = 1, UpdatedId = 2, DeletedId = 3, NotFoundId = int.MaxValue;
    private static readonly string TestNotFoundMessage = "course not found by id: " + NotFoundId;

    private const string TestData = """
                                    INSERT INTO courses (name, description) VALUES ('read', 'read'),
                                                                                   ('should-updated', 'should-updated'),
                                                                                   ('deleted', 'deleted')
                                    """;

    private static bool _testDataInitialized;

    private static readonly Course Read = new() { Name = "read", Description = "read" },
        Created = new() { Name = "created", Description = "created" },
        Updated = new() { Name = "updated", Description = "updated" };

    private static readonly PostgreSqlContainer Container = new PostgreSqlBuilder().WithImage("postgres").Build();
    private const string Url = "api/Courses";

    private HttpClient _client = null!;
    private WebApplicationFactory<Program> _factory = null!;

    public async Task InitializeAsync()
    {
        await Container.StartAsync();

        string connectionString = Container.GetConnectionString();

        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<CoursesDbContext>(options => options.UseNpgsql(connectionString));
                });
            }
        );
        _client = _factory.CreateClient();

        using IServiceScope scope = _factory.Services.CreateScope();
        CoursesDbContext context = scope.ServiceProvider.GetRequiredService<CoursesDbContext>();
        await context.Database.MigrateAsync();

        if (_testDataInitialized)
        {
            return;
        }

        await using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        await using NpgsqlCommand command = new(TestData, connection);
        command.ExecuteNonQuery();

        _testDataInitialized = true;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task TestReadAll()
    {
        HttpResponseMessage response = await _client.GetAsync(Url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string content = await response.Content.ReadAsStringAsync();
        var courses = JsonConvert.DeserializeObject<Course[]>(content);
        Assert.NotNull(courses);
        Assert.NotEmpty(courses);
        AssertCourse(Read, courses[0]);
    }

    [Fact]
    public async Task TestRead()
    {
        HttpResponseMessage response = await _client.GetAsync(Url + "/" + ReadId);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string content = await response.Content.ReadAsStringAsync();
        Course? course = JsonConvert.DeserializeObject<Course>(content);
        AssertCourse(Read, course);
    }

    [Fact]
    public async Task TestReadNotFound()
    {
        HttpResponseMessage response = await _client.GetAsync(Url + "/" + NotFoundId);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string content = await response.Content.ReadAsStringAsync();
        string? errorMessage = JsonConvert.DeserializeObject<string>(content);
        Assert.Equal(TestNotFoundMessage, errorMessage);
    }

    [Fact]
    public async Task TestCreate()
    {
        string courseJson = JsonConvert.SerializeObject(Created);
        HttpContent content = new StringContent(courseJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync(Url, content);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        string responseContent = await response.Content.ReadAsStringAsync();
        Course? course = JsonConvert.DeserializeObject<Course>(responseContent);
        AssertCourse(Created, course);
    }

    [Fact]
    public async Task TestUpdate()
    {
        string courseJson = JsonConvert.SerializeObject(Updated);
        HttpContent content = new StringContent(courseJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync(Url + "/" + UpdatedId, content);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task TestUpdateNotFound()
    {
        string courseJson = JsonConvert.SerializeObject(Updated);
        HttpContent content = new StringContent(courseJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync(Url + "/" + NotFoundId, content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseContent = await response.Content.ReadAsStringAsync();
        string? errorMessage = JsonConvert.DeserializeObject<string>(responseContent);
        Assert.Equal(TestNotFoundMessage, errorMessage);
    }

    [Fact]
    public async Task TestDelete()
    {
        HttpResponseMessage response = await _client.DeleteAsync(Url + "/" + DeletedId);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task TestDeleteNotFound()
    {
        HttpResponseMessage response = await _client.DeleteAsync(Url + "/" + NotFoundId);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseContent = await response.Content.ReadAsStringAsync();
        string? message = JsonConvert.DeserializeObject<string>(responseContent);
        Assert.Equal(TestNotFoundMessage, message);
    }

    private static void AssertCourse(Course expected, Course? actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Description, actual.Description);
    }
}