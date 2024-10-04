using BLL.Exceptions;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class CourseService(ICourseRepository repository) : ICourseService
{
    public async Task<int> CreateAsync(Course course)
    {
        return await repository.CreateAsync(course);
    }

    public async Task<IEnumerable<Course>> ReadAllAsync()
    {
        var courses = await repository.ReadAllAsync();
        return courses;
    }

    public async Task<Course> ReadAsync(int id)
    {
        Course? course = await repository.ReadAsync(id);
        return course ?? throw new CourseNotFoundException("course not found by id: " + id);
    }

    public async Task UpdateAsync(int id, Course course)
    {
        await ReadAsync(id);

        await repository.UpdateAsync(id, course);
    }

    public async Task DeleteAsync(int id)
    {
        await ReadAsync(id);

        await repository.DeleteAsync(id);
    }
}