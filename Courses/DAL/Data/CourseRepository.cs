using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CourseRepository(NpgsqlConnection connection)
{
    public async Task Create(Course course)
    {
        await connection.OpenAsync();

        const string insertCourse = "insert into courses (name, description) values (@Name, @Description)";
        var param = new { course.Name, course.Description };
        await connection.ExecuteAsync(insertCourse, param);

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        await connection.OpenAsync();

        const string selectCourses = "select * from courses";
        var courses = await connection.QueryAsync<Course>(selectCourses);

        await connection.CloseAsync();
        return courses;
    }

    public async Task<Course?> GetById(int id)
    {
        await connection.OpenAsync();

        const string selectCourseById = "select * from courses where id = @Id";
        var param = new { id };
        Course? course = await connection.QuerySingleOrDefaultAsync<Course>(selectCourseById, param);

        await connection.CloseAsync();
        return course;
    }

    public async Task UpdateById(int id, Course course)
    {
        await connection.OpenAsync();

        const string updateCourseById = "update courses set name = @Name, description = @Description where id = @Id";
        var param = new { course.Name, course.Description, id };
        await connection.ExecuteAsync(updateCourseById, param);

        await connection.CloseAsync();
    }

    public async Task DeleteById(int id)
    {
        await connection.OpenAsync();

        const string deleteCourseById = "delete from courses where id = @Id";
        var param = new { id };
        await connection.ExecuteAsync(deleteCourseById, param);

        await connection.CloseAsync();
    }
}