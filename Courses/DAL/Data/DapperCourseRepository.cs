using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class DapperCourseRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(Course course)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        return await connection.ExecuteScalarAsync<int>(
            "insert into courses (name, description) values (@Name, @Description) returning id",
            new { course.Name, course.Description }
        );
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        return await connection.QueryAsync<Course>("select * from courses");
    }

    public async Task<Course?> GetAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Course>(
            "select * from courses where id = @Id",
            new { id }
        );
    }

    public async Task UpdateAsync(int id, Course course)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync(
            "update courses set name = @Name, description = @Description where id = @Id",
            new { course.Name, course.Description, id }
        );
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync("delete from courses where id = @Id", new { id });
    }
}