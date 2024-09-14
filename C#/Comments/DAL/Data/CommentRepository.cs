using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CommentRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(Comment comment)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string insertQuery = "insert into comments(author, content) values (@author, @content) returning id";
        object parameters = new { comment.Author, comment.Content };
        return await connection.ExecuteScalarAsync<int>(insertQuery, parameters);
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string selectAllQuery = "select * from comments";
        return await connection.QueryAsync<Comment>(selectAllQuery);
    }

    public async Task<Comment?> GetAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string selectQuery = "select * from comments where id = @id";
        object parameter = new { id };
        return await connection.QuerySingleOrDefaultAsync<Comment>(selectQuery, parameter);
    }

    public async Task UpdateAsync(int id, Comment comment)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string updateQuery = "update comments set author = @author, content = @content where id = @id";
        object parameters = new { comment.Author, comment.Content, id };
        await connection.ExecuteAsync(updateQuery, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string deleteQuery = "delete from comments where id = @id";
        object parameter = new { id };
        await connection.ExecuteAsync(deleteQuery, parameter);
    }
}