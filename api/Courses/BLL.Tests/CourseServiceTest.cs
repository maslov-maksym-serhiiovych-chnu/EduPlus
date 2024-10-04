using BLL.Exceptions;
using BLL.Services;
using DAL.Data;
using DAL.Models;
using Moq;

namespace BLL.Tests;

public class CourseServiceTest
{
    private const int TestId = 1;

    private static readonly Course TestCourse = new() { Name = "test", Description = "test" },
        UpdatedCourse = new() { Name = "updated", Description = "updated" };

    private readonly Mock<ICourseRepository> _mockRepository;
    private readonly CourseService _service;

    public CourseServiceTest()
    {
        _mockRepository = new Mock<ICourseRepository>();
        _service = new CourseService(_mockRepository.Object);
    }

    [Fact]
    public async Task TestCreateAsync()
    {
        _mockRepository.Setup(repository => repository.CreateAsync(TestCourse)).ReturnsAsync(TestId);

        int id = await _service.CreateAsync(TestCourse);
        Assert.Equal(TestId, id);
    }

    [Fact]
    public async Task TestReadAllAsync()
    {
        var courses = new[] { TestCourse };
        _mockRepository.Setup(repository => repository.ReadAllAsync()).ReturnsAsync(courses);

        var actualCourses = await _service.ReadAllAsync();
        Assert.Equal(courses, actualCourses);
    }

    [Fact]
    public async Task TestReadAsync()
    {
        _mockRepository.Setup(repository => repository.ReadAsync(TestId)).ReturnsAsync(TestCourse);

        Course course = await _service.ReadAsync(TestId);
        Assert.Equal(TestCourse.Name, course.Name);
        Assert.Equal(TestCourse.Description, course.Description);
    }

    [Fact]
    public async Task TestReadAsyncNotFound()
    {
        _mockRepository.Setup(repository => repository.ReadAsync(TestId)).ReturnsAsync((Course?)null);

        await Assert.ThrowsAsync<CourseNotFoundException>(async () => await _service.ReadAsync(TestId));
    }

    [Fact]
    public async Task TestUpdateAsync()
    {
        _mockRepository.Setup(repository => repository.ReadAsync(TestId)).ReturnsAsync(TestCourse);

        await _service.UpdateAsync(TestId, UpdatedCourse);
        _mockRepository.Verify(repository => repository.UpdateAsync(TestId, UpdatedCourse), Times.Once);
    }

    [Fact]
    public async Task TestUpdateAsyncNotFound()
    {
        _mockRepository.Setup(repository => repository.ReadAsync(TestId)).ReturnsAsync((Course?)null);

        await Assert.ThrowsAsync<CourseNotFoundException>(async () => await _service.UpdateAsync(TestId, UpdatedCourse));
    }

    [Fact]
    public async Task TestDeleteAsync()
    {
        _mockRepository.Setup(repository => repository.ReadAsync(TestId)).ReturnsAsync(TestCourse);

        await _service.DeleteAsync(TestId);
        _mockRepository.Verify(repository => repository.DeleteAsync(TestId), Times.Once);
    }

    [Fact]
    public async Task TestDeleteAsyncNotFound()
    {
        _mockRepository.Setup(repository => repository.ReadAsync(TestId)).ReturnsAsync((Course?)null);

        await Assert.ThrowsAsync<CourseNotFoundException>(async () => await _service.DeleteAsync(TestId));
    }
}