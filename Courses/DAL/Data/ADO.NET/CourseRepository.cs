using System.Data;
using DAL.Models;
using Npgsql;

namespace DAL.Data.ADO.NET;

public class CourseRepository(NpgsqlConnection connection)
{
    public async Task CreateAsync(Course course)
    {
        await connection.OpenAsync();

        const string insertCourse = "insert into courses (name, description) values ($1, $2)";
        await using NpgsqlCommand command = new(insertCourse, connection);
        command.Parameters.Add(new NpgsqlParameter { Value = course.Name });
        command.Parameters.Add(new NpgsqlParameter { Value = course.Description });

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        await connection.OpenAsync();

        const string selectCourses = "select * from courses";
        await using NpgsqlCommand command = new(selectCourses, connection);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        var courses = new List<Course>();
        while (await reader.ReadAsync())
        {
            Course course = new()
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Description = reader.GetString(reader.GetOrdinal("description"))
            };
            courses.Add(course);
        }

        await connection.CloseAsync();
        return courses;
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        await connection.OpenAsync();

        const string selectCourseById = "select * from courses where id = $1";
        await using NpgsqlCommand command = new(selectCourseById, connection);
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        Course? course = null;
        if (await reader.ReadAsync())
        {
            course = new Course
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Description = reader.GetString(reader.GetOrdinal("description"))
            };
        }

        await connection.CloseAsync();
        return course;
    }

    public async Task UpdateByIdAsync(int id, Course course)
    {
        await connection.OpenAsync();

        const string updateCourseById = "update courses set name = $1, description = $2 where id = $3";
        await using NpgsqlCommand command = new(updateCourseById, connection);
        command.Parameters.Add(new NpgsqlParameter { Value = course.Name });
        command.Parameters.Add(new NpgsqlParameter { Value = course.Description });
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        await connection.OpenAsync();

        const string deleteCourseById = "delete from courses where id = $1";
        await using NpgsqlCommand command = new(deleteCourseById, connection);
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }
}