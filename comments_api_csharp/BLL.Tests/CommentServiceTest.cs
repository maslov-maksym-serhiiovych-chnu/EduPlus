using BLL.Exceptions;
using BLL.Services;
using DAL.Data;
using DAL.Models;
using Moq;

namespace BLL.Tests;

public class CommentServiceTest
{
    private static readonly Mock<ICommentRepository> RepositoryMock = new();
    private static readonly CommentService Service = new(RepositoryMock.Object);

    [Fact]
    public async Task TestReadNotExistingAsync()
    {
        int id = It.IsAny<int>();
        RepositoryMock.Setup(repository => repository.ReadAsync(id)).ReturnsAsync((Comment?)null);
        await Assert.ThrowsAsync<CommentNotFoundException>(async () => await Service.ReadAsync(id));
    }

    [Fact]
    public async Task TestUpdateNotExistingAsync()
    {
        int id = It.IsAny<int>();
        Comment updated = It.IsAny<Comment>();
        RepositoryMock.Setup(repository => repository.ReadAsync(id)).ReturnsAsync((Comment?)null);
        await Assert.ThrowsAsync<CommentNotFoundException>(async () => await Service.UpdateAsync(id, updated));
    }

    [Fact]
    public async Task TestDeleteNotExistingAsync()
    {
        int id = It.IsAny<int>();
        RepositoryMock.Setup(repository => repository.ReadAsync(id)).ReturnsAsync((Comment?)null);
        await Assert.ThrowsAsync<CommentNotFoundException>(async () => await Service.DeleteAsync(id));
    }
}