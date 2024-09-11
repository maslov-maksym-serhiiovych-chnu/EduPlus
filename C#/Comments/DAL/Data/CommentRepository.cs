using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CommentRepository(NpgsqlDataSource dataSource)
{
    public async Task<int> CreateAsync(Comment comment)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string insertComment = "insert into comments(author, content) values ($1, $2) returning id";
        object parameters = new { comment.Author, comment.Content };
        return await connection.ExecuteScalarAsync<int>(insertComment, parameters);
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string selectAll = "select * from comments";
        return await connection.QueryAsync<Comment>(selectAll);
    }
}