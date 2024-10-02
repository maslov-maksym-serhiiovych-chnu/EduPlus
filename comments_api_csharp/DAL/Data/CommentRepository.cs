using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CommentRepository(NpgsqlDataSource dataSource) : ICommentRepository
{
    public async Task<int> CreateAsync(Comment comment)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string sql = "INSERT INTO comments(author, content) VALUES (@author, @content) RETURNING id";
        object parameters = new { comment.Author, comment.Content };
        return await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task<IEnumerable<Comment>> ReadAllAsync()
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string sql = "SELECT * FROM comments";
        return await connection.QueryAsync<Comment>(sql);
    }

    public async Task<Comment?> ReadAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string sql = "SELECT * FROM comments WHERE id = @id";
        object parameter = new { id };
        return await connection.QuerySingleOrDefaultAsync<Comment>(sql, parameter);
    }

    public async Task UpdateAsync(int id, Comment comment)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string sql = "UPDATE comments SET author = @author, content = @content WHERE id = @id";
        object parameters = new { comment.Author, comment.Content, id };
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync();

        const string sql = "DELETE FROM comments WHERE id = @id";
        object parameter = new { id };
        await connection.ExecuteAsync(sql, parameter);
    }
}