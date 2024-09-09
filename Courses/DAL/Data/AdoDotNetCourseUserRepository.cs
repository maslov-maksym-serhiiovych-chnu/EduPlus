using DAL.Models;
using Npgsql;

namespace DAL.Data;

public class AdoDotNetCourseUserRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(CourseUser courseUser)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new(
            "insert into course_users (course_id) values ($1) returning id",
            connection
        );
        command.Parameters.Add(new NpgsqlParameter { Value = courseUser.CourseId });

        return (int)(await command.ExecuteScalarAsync() ?? -1);
    }

    public async Task<IEnumerable<CourseUser>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new("select * from course_users", connection);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var courseUsers = new List<CourseUser>();
        while (await reader.ReadAsync())
        {
            CourseUser courseUser = new()
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                CourseId = reader.GetInt32(reader.GetOrdinal("course_id"))
            };
            courseUsers.Add(courseUser);
        }

        return courseUsers;
    }

    public async Task<CourseUser?> GetAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new("select * from course_users where id = $1", connection);
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        CourseUser? courseUser = null;
        if (await reader.ReadAsync())
        {
            courseUser = new CourseUser
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                CourseId = reader.GetInt32(reader.GetOrdinal("course_id"))
            };
        }

        return courseUser;
    }

    public async Task UpdateAsync(int id, CourseUser courseUser)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new(
            "update course_users set course_id = $1 where id = $2",
            connection
        );
        command.Parameters.Add(new NpgsqlParameter { Value = courseUser.CourseId });
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        await using NpgsqlCommand command = new("delete from course_users where id = $1", connection);
        command.Parameters.Add(new NpgsqlParameter { Value = id });

        await command.ExecuteNonQueryAsync();
    }
}