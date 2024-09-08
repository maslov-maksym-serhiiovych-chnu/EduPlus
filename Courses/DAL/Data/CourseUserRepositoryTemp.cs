using DAL.Models;
using Npgsql;

namespace DAL.Data;

public class CourseUserRepositoryTemp(NpgsqlConnection connection)
{
    public async Task Create(CourseUser courseUser)
    {
        await connection.OpenAsync();

        const string insertCourseUser = "insert into course_users (course_id) values (@CourseId)";
        await using NpgsqlCommand command = new(insertCourseUser, connection);
        command.Parameters.Add(new NpgsqlParameter("CourseId", courseUser.CourseId));

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task<IEnumerable<CourseUser>> GetAll()
    {
        await connection.OpenAsync();

        const string selectCourseUsers = "select * from course_users";
        await using NpgsqlCommand command = new(selectCourseUsers, connection);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        var courseUsers = new List<CourseUser>();
        while (await reader.ReadAsync())
        {
            CourseUser courseUser = new()
            {
                CourseId = reader.GetInt32(reader.GetOrdinal("course_id")),
            };
            courseUsers.Add(courseUser);
        }

        await connection.CloseAsync();
        return courseUsers;
    }

    public async Task<CourseUser?> GetById(int id)
    {
        await connection.OpenAsync();

        const string selectCourseUserById = "select * from course_users where id = @Id";
        await using NpgsqlCommand command = new(selectCourseUserById, connection);
        command.Parameters.Add(new NpgsqlParameter("Id", id));

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        CourseUser? courseUser = null;
        if (await reader.ReadAsync())
        {
            courseUser = new CourseUser
            {
                CourseId = reader.GetInt32(reader.GetOrdinal("course_id"))
            };
        }

        await connection.CloseAsync();
        return courseUser;
    }

    public async Task UpdateById(int id, CourseUser courseUser)
    {
        await connection.OpenAsync();

        const string updateCourseUserById = "update course_users set course_id = @CourseId where id = @Id";
        await using NpgsqlCommand command = new(updateCourseUserById, connection);
        command.Parameters.Add(new NpgsqlParameter("CourseId", courseUser.CourseId));
        command.Parameters.Add(new NpgsqlParameter("Id", id));

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task DeleteById(int id)
    {
        await connection.OpenAsync();

        const string deleteCourseUserById = "delete from course_users where id = @Id";
        await using NpgsqlCommand command = new(deleteCourseUserById, connection);
        command.Parameters.Add(new NpgsqlParameter("Id", id));

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }
}