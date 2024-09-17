using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CourseRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(Course course)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string insertQuery = "insert into courses(name, description) values (@name, @description) returning id";
        object parameters = new { course.Name, course.Description };
        return await connection.ExecuteScalarAsync<int>(insertQuery, parameters);
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string selectAllQuery = "select * from courses";
        return await connection.QueryAsync<Course>(selectAllQuery);
    }

    public async Task<Course?> GetAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string selectQuery = "select * from courses where id = @id";
        object parameter = new { id };
        return await connection.QuerySingleOrDefaultAsync<Course>(selectQuery, parameter);
    }

    public async Task UpdateAsync(int id, Course course)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string updateQuery = "update courses set name = @name, description = @description where id = @id";
        object parameters = new { course.Name, course.Description, id };
        await connection.ExecuteAsync(updateQuery, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string deleteQuery = "delete from courses where id = @id";
        object parameter = new { id };
        await connection.ExecuteAsync(deleteQuery, parameter);
    }
}