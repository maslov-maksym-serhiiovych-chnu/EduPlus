using BLL.Exceptions;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class CourseService(CoursesDbContext context)
{
    public async Task<int> CreateAsync(Course course)
    {
        await context.Courses.AddAsync(course);

        return await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Course>> ReadAllAsync()
    {
        var courses = await context.Courses.ToArrayAsync();
        return courses;
    }

    public async Task<Course> ReadAsync(int id)
    {
        Course? course = await context.Courses.FindAsync(id);
        return course ?? throw new CourseNotFoundException("course not found by id: " + id);
    }

    public async Task UpdateAsync(int id, Course course)
    {
        Course updated = await ReadAsync(id);
        updated.Name = course.Name;
        updated.Description = course.Description;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Course course = await ReadAsync(id);
        context.Courses.Remove(course);
    }
}