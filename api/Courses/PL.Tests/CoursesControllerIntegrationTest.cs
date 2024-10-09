using System.Net;
using System.Text;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Testcontainers.PostgreSql;
using StringContent = System.Net.Http.StringContent;

namespace PL.Tests;

public class CoursesControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private static readonly PostgreSqlContainer Container = new PostgreSqlBuilder().WithImage("postgres").Build();

    private const string Url = "api/Courses";

    private const int NotFoundCourseId = int.MaxValue;
    private static int _shouldUpdatedCourseId, _deletedCourseId;

    private static readonly string NotFoundCourseMessage = "course not found by id: " + NotFoundCourseId;
    private static string _deletedCourseNotFoundMessage = null!;

    private static bool _testDataCreated;

    private static readonly Course ReadCourse = new() { Name = "read", Description = "read" },
        ShouldUpdatedCourse = new() { Name = "should-updated", Description = "should-updated" },
        DeletedCourse = new() { Name = "deleted", Description = "deleted" },
        CreatedCourse = new() { Name = "created", Description = "created" },
        UpdatedCourse = new() { Name = "updated", Description = "updated" };

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

        if (_testDataCreated)
        {
            return;
        }

        await CreateAsync(ReadCourse);
        Course? shouldUpdatedCourse = await CreateAsync(ShouldUpdatedCourse);
        _shouldUpdatedCourseId = shouldUpdatedCourse?.Id ?? -1;
        Course? deletedCourse = await CreateAsync(DeletedCourse);
        _deletedCourseId = deletedCourse?.Id ?? -1;
        _deletedCourseNotFoundMessage = "course not found by id: " + _deletedCourseId;

        _testDataCreated = true;
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
        AssertCourse(ReadCourse, courses[0]);
    }

    [Fact]
    public async Task TestCreate()
    {
        Course? created = await CreateAsync(CreatedCourse);
        AssertCourse(CreatedCourse, created);

        int id = created?.Id ?? -1;
        await AssertReadAsync(CreatedCourse, id);
    }

    [Fact]
    public async Task TestUpdate()
    {
        string updatedCourseJson = JsonConvert.SerializeObject(UpdatedCourse);
        HttpContent updatedCourseContent = new StringContent(updatedCourseJson, Encoding.UTF8, "application/json");
        HttpResponseMessage updatedCourseResponse = await _client.PutAsync(
            Url + "/" + _shouldUpdatedCourseId, updatedCourseContent
        );
        Assert.Equal(HttpStatusCode.NoContent, updatedCourseResponse.StatusCode);
        await AssertReadAsync(UpdatedCourse, _shouldUpdatedCourseId);
    }

    [Fact]
    public async Task TestUpdateNotFound()
    {
        string courseJson = JsonConvert.SerializeObject(UpdatedCourse);
        HttpContent content = new StringContent(courseJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync(Url + "/" + NotFoundCourseId, content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        await AssertNotFoundAsync(NotFoundCourseMessage, response);
    }

    [Fact]
    public async Task TestDelete()
    {
        HttpResponseMessage deletedCourseResponse = await _client.DeleteAsync(Url + "/" + _deletedCourseId);
        Assert.Equal(HttpStatusCode.NoContent, deletedCourseResponse.StatusCode);

        HttpResponseMessage response = await _client.GetAsync(Url + "/" + _deletedCourseId);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        await AssertNotFoundAsync(_deletedCourseNotFoundMessage, response);
    }

    [Fact]
    public async Task TestDeleteNotFound()
    {
        HttpResponseMessage response = await _client.DeleteAsync(Url + "/" + NotFoundCourseId);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        await AssertNotFoundAsync(NotFoundCourseMessage, response);
    }

    private static void AssertCourse(Course expected, Course? actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Description, actual.Description);
    }

    private async Task AssertReadAsync(Course expected, int id)
    {
        HttpResponseMessage response = await _client.GetAsync(Url + "/" + id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string content = await response.Content.ReadAsStringAsync();
        Course? course = JsonConvert.DeserializeObject<Course>(content);
        AssertCourse(expected, course);
    }

    private static async Task AssertNotFoundAsync(string errorMessage, HttpResponseMessage response)
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        string? responseErrorMessage = JsonConvert.DeserializeObject<string>(responseContent);
        Assert.Equal(errorMessage, responseErrorMessage);
    }

    private async Task<Course?> CreateAsync(Course course)
    {
        string courseJson = JsonConvert.SerializeObject(course);
        HttpContent courseContent = new StringContent(courseJson, Encoding.UTF8, "application/json");
        HttpResponseMessage courseResponse = await _client.PostAsync(Url, courseContent);
        string courseStr = await courseResponse.Content.ReadAsStringAsync();
        Course? created = JsonConvert.DeserializeObject<Course>(courseStr);
        return created;
    }
}