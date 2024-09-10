using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class DapperCourseUserRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(CourseUser courseUser)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        return await connection.ExecuteScalarAsync<int>(
            "insert into course_users (course_id) values (@CourseId) returning id",
            new { courseUser.CourseId }
        );
    }

    // TODO: fix
    public async Task<IEnumerable<CourseUser>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        return await connection.QueryAsync<CourseUser>("select * from course_users");
    }

    // TODO: fix
    public async Task<CourseUser?> GetAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<CourseUser>(
            "select * from course_users where id = @Id",
            new { id }
        );
    }

    public async Task UpdateAsync(int id, CourseUser courseUser)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync(
            "update course_users set course_id = @CourseId where id = @Id",
            new { courseUser.CourseId, id }
        );
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync("delete from course_users where id = @Id", new { id });
    }
}