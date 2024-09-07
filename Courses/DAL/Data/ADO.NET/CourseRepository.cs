using System.Data;
using DAL.Models;
using Npgsql;

namespace DAL.Data.ADO.NET;

public class CourseRepository(NpgsqlConnection connection)
{
    public async Task Save(Course course)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("insert_course", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new NpgsqlParameter("p_name", course.Name));
        command.Parameters.Add(new NpgsqlParameter("p_description", course.Description));

        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> FindAll()
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("SELECT * FROM courses", connection);
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

    public async Task<Course?> FindById(int id)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("SELECT * FROM courses WHERE id=@id", connection);
        command.Parameters.Add(new NpgsqlParameter("id", id));

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

        await using NpgsqlCommand command = new("update_course_by_id", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new NpgsqlParameter("p_id", id));
        command.Parameters.Add(new NpgsqlParameter("p_name", course.Name));
        command.Parameters.Add(new NpgsqlParameter("p_description", course.Description));

        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }

    public async Task DeleteById(int id)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("delete_course_by_id", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new NpgsqlParameter("p_id", id));

        await connection.CloseAsync();
    }
}