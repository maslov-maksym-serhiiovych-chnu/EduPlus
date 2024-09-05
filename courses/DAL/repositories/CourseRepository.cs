using System.Data;
using DAL.models;
using Npgsql;

namespace DAL.repositories;

public class CourseRepository(NpgsqlConnection connection)
{
    public async Task Create(Course created)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("create_course", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new NpgsqlParameter("p_name", created.Name));
        command.Parameters.Add(new NpgsqlParameter("p_description", created.Description));

        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> ReadAll()
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("select * from courses", connection);
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

    public async Task<Course> ReadById(int id)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("select * from courses where id=@id", connection);
        command.Parameters.Add(new NpgsqlParameter("id", id));

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        Course course = null!;
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

    public async Task Update(int id, Course updated)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("update_course", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new NpgsqlParameter("p_id", updated.Id));
        command.Parameters.Add(new NpgsqlParameter("p_name", updated.Name));
        command.Parameters.Add(new NpgsqlParameter("p_description", updated.Description));

        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }

    public async Task Delete(int id)
    {
        await connection.OpenAsync();

        await using NpgsqlCommand command = new("update_course", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new NpgsqlParameter("p_id", id));

        await connection.CloseAsync();
    }
}