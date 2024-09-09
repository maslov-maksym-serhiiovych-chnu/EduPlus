using DAL.Models;
using Npgsql;

namespace DAL.Data;

public class AdoDotNetCourseRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(Course course)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new(
            "insert into courses (name, description) values ($1, $2) returning id",
            connection
        );
        command.Parameters.Add(new NpgsqlParameter { Value = course.Name });
        command.Parameters.Add(new NpgsqlParameter { Value = course.Description });

        return (int)(await command.ExecuteScalarAsync() ?? -1);
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

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

        return courses;
    }

    public async Task<Course?> GetAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new("select * from courses where id = $1", connection);
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

        return course;
    }

    public async Task UpdateAsync(int id, Course course)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new(
            "update courses set name = $1, description = $2 where id = $3",
            connection
        );
        command.Parameters.Add(new NpgsqlParameter { Value = course.Name });
        command.Parameters.Add(new NpgsqlParameter { Value = course.Description });
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new("delete from courses where id = $1", connection);
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await command.ExecuteNonQueryAsync();
    }
}