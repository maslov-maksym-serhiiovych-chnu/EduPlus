using BLL.DTOs;
using BLL.Exceptions;
using DAL.Data;
using DAL.Models;

namespace BLL.Services;

public class CourseService(CoursesDbContext context)
{
    public async Task<int> CreateAsync(Course course)
    {
        await context.Courses.AddAsync(course);

        return await context.SaveChangesAsync();
    }

    public IEnumerable<Course> ReadAll(CourseQueryParameters parameters)
    {
        string? searchTerm = parameters.SearchTerm;
        var courses = context.Courses.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            courses = courses.Where(c => c.Name.Contains(searchTerm) ||
                                         (c.Description != null && c.Description.Contains(searchTerm)));
        }

        string? sortBy = parameters.SortBy;
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            courses = sortBy.ToLower() switch
            {
                "name" => courses.OrderBy(c => c.Name),
                "description" => courses.OrderBy(c => c.Description),
                _ => courses
            };
        }

        int pageIndex = parameters.PageIndex, pageSize = parameters.PageSize;
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }

        courses = courses.Skip((pageIndex - 1) * pageSize).Take(pageSize);
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