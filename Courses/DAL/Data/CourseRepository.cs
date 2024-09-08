using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CourseRepository(NpgsqlConnection connection)
{
    public async Task CreateAsync(Course course)
    {
        await connection.OpenAsync();

        const string insertCourse = "insert into courses (name, description) values ($1, $2)";
        var param = new { course.Name, course.Description };
        await connection.ExecuteAsync(insertCourse, param);

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        await connection.OpenAsync();

        const string selectCourses = "select * from courses";
        var courses = await connection.QueryAsync<Course>(selectCourses);

        await connection.CloseAsync();
        return courses;
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        await connection.OpenAsync();

        const string selectCourseById = "select * from courses where id = $1";
        var param = new { id };
        Course? course = await connection.QuerySingleOrDefaultAsync<Course>(selectCourseById, param);

        await connection.CloseAsync();
        return course;
    }

    public async Task UpdateByIdAsync(int id, Course course)
    {
        await connection.OpenAsync();

        const string updateCourseById = "update courses set name = $1, description = $2 where id = $3";
        var param = new { course.Name, course.Description, id };
        await connection.ExecuteAsync(updateCourseById, param);

        await connection.CloseAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        await connection.OpenAsync();

        const string deleteCourseById = "delete from courses where id = $1";
        var param = new { id };
        await connection.ExecuteAsync(deleteCourseById, param);

        await connection.CloseAsync();
    }
}