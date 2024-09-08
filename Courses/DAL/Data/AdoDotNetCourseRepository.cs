using DAL.Models;
using Npgsql;

namespace DAL.Data;

public class AdoDotNetCourseRepository(NpgsqlConnection connection)
{
    public async Task Create(Course course)
    {
        await connection.OpenAsync();

        const string insertCourse = "insert into courses (name, description) values (@Name, @Description)";
        await using NpgsqlCommand command = new(insertCourse, connection);
        command.Parameters.Add(new NpgsqlParameter("Name", course.Name));
        command.Parameters.Add(new NpgsqlParameter("Description", course.Description));

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> GetAll()
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

    public async Task<Course?> GetById(int id)
    {
        await connection.OpenAsync();

        const string selectCourseById = "select * from courses where id = @Id";
        await using NpgsqlCommand command = new(selectCourseById, connection);
        command.Parameters.Add(new NpgsqlParameter("Id", id));

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

    public async Task UpdateById(int id, Course course)
    {
        await connection.OpenAsync();

        const string updateCourseById = "update courses set name = @Name, description = @Description where id = @Id";
        await using NpgsqlCommand command = new(updateCourseById, connection);
        command.Parameters.Add(new NpgsqlParameter("Name", course.Name));
        command.Parameters.Add(new NpgsqlParameter("Description", course.Description));
        command.Parameters.Add(new NpgsqlParameter("Id", id));

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task DeleteById(int id)
    {
        await connection.OpenAsync();

        const string deleteCourseById = "delete from courses where id = @Id";
        await using NpgsqlCommand command = new(deleteCourseById, connection);
        command.Parameters.Add(new NpgsqlParameter("Id", id));

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }
}