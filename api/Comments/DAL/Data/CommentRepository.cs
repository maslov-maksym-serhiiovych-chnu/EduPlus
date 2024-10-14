using DAL.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace DAL.Data;

public class CommentRepository(string? connectionString)
{
    public async Task InitializeAsync()
    {
        await using SqliteConnection connection = new(connectionString);
        await connection.OpenAsync();
        const string sql = "CREATE TABLE IF NOT EXISTS comments (id INTEGER PRIMARY KEY AUTOINCREMENT, content TEXT)";
        await connection.ExecuteAsync(sql);
    }

    public async Task<int> CreateAsync(Comment comment)
    {
        await using SqliteConnection connection = new(connectionString);
        await connection.OpenAsync();
        const string sql = "INSERT INTO comments (content) VALUES (@content); SELECT last_insert_rowid()";
        object parameter = new { content = comment.Content };
        int id = await connection.ExecuteScalarAsync<int>(sql, parameter);
        return id;
    }

    public async Task<IEnumerable<Comment>> ReadAllAsync()
    {
        await using SqliteConnection connection = new(connectionString);
        await connection.OpenAsync();
        const string sql = "SELECT * FROM comments";
        var comments = await connection.QueryAsync<Comment>(sql);
        return comments;
    }

    public async Task<Comment?> ReadAsync(int id)
    {
        await using SqliteConnection connection = new(connectionString);
        await connection.OpenAsync();
        const string sql = "SELECT * FROM comments WHERE id = @id";
        object parameter = new { id };
        Comment? comment = await connection.QuerySingleOrDefaultAsync<Comment>(sql, parameter);
        return comment;
    }

    public async Task UpdateAsync(int id, Comment comment)
    {
        await using SqliteConnection connection = new(connectionString);
        await connection.OpenAsync();
        const string sql = "UPDATE comments SET content = @content WHERE id = @id";
        object parameters = new { content = comment.Content, id };
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(int id)
    {
        await using SqliteConnection connection = new(connectionString);
        await connection.OpenAsync();
        const string sql = "DELETE FROM comments WHERE id = @id";
        object parameter = new { id };
        await connection.ExecuteAsync(sql, parameter);
    }
}