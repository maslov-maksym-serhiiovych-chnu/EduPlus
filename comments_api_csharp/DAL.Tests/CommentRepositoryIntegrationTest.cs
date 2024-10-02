using DAL.Data;
using DAL.Models;
using Dapper;
using Npgsql;
using Testcontainers.PostgreSql;

namespace DAL.Tests;

public class CommentRepositoryIntegrationTest : IAsyncLifetime
{
    private const string TestData = """
                                    INSERT INTO comments (author, content) VALUES ('read', 'read'),
                                                                                  ('should-updated', 'should-updated'),
                                                                                  ('deleted', 'deleted')
                                    """;

    private static bool _testDataCreated;

    private const int ReadId = 1, UpdatedId = 2, DeletedId = 3;

    private static readonly Comment CreatedComment = new() { Author = "created", Content = "created" },
        ReadComment = new() { Author = "read", Content = "read" },
        UpdatedComment = new() { Author = "updated", Content = "updated" };

    private static readonly PostgreSqlContainer Container = new PostgreSqlBuilder()
        .WithImage("postgres")
        .Build();

    private NpgsqlDataSource _dataSource = null!;
    private CommentRepository _repository = null!;

    public async Task InitializeAsync()
    {
        await Container.StartAsync();

        string connectionString = Container.GetConnectionString();
        _dataSource = NpgsqlDataSource.Create(connectionString);
        _repository = new CommentRepository(_dataSource);

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync();
        const string initSqlPath = "../../../../DAL/Data/init.sql";
        string initSql = await File.ReadAllTextAsync(initSqlPath);
        await connection.ExecuteAsync(initSql);

        if (_testDataCreated) return;
        await connection.ExecuteAsync(TestData);
        _testDataCreated = true;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task TestCreateAsync()
    {
        int id = await _repository.CreateAsync(CreatedComment);
        Comment? comment = await _repository.ReadAsync(id);
        AssertComment(CreatedComment, comment);
    }

    [Fact]
    public async Task TestReadAllAsync()
    {
        var comments = await _repository.ReadAllAsync();
        var commentsArr = comments as Comment[] ?? comments.ToArray();
        Assert.True(commentsArr.Length != 0);

        Comment? first = commentsArr.FirstOrDefault();
        AssertComment(ReadComment, first);
    }

    [Fact]
    public async Task TestReadAsync()
    {
        Comment? comment = await _repository.ReadAsync(ReadId);
        AssertComment(ReadComment, comment);
    }

    [Fact]
    public async Task TestUpdateAsync()
    {
        await _repository.UpdateAsync(UpdatedId, UpdatedComment);

        Comment? comment = await _repository.ReadAsync(UpdatedId);
        AssertComment(UpdatedComment, comment);
    }

    [Fact]
    public async Task TestDeleteAsync()
    {
        await _repository.DeleteAsync(DeletedId);

        Comment? comment = await _repository.ReadAsync(DeletedId);
        Assert.Null(comment);
    }

    private static void AssertComment(Comment expected, Comment? actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.Author, actual.Author);
        Assert.Equal(expected.Content, actual.Content);
    }
}