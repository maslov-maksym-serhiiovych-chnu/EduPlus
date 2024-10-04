using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class CourseRepository(CoursesDbContext context) : ICourseRepository
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

    public async Task<Course?> ReadAsync(int id)
    {
        Course? course = await context.Courses.FindAsync(id);
        return course;
    }

    public async Task UpdateAsync(int id, Course course)
    {
        Course? updated = await ReadAsync(id);
        if (updated == null)
        {
            return;
        }

        updated.Name = course.Name;
        updated.Description = course.Description;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Course? course = await ReadAsync(id);
        if (course == null)
        {
            return;
        }

        context.Courses.Remove(course);

        await context.SaveChangesAsync();
    }
}