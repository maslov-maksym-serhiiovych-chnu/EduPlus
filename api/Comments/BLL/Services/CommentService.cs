using BLL.Exceptions;
using DAL.Data;
using DAL.Models;

namespace BLL.Services;

public class CommentService(CommentRepository repository)
{
    public async Task<int> CreateAsync(Comment comment)
    {
        int id = await repository.CreateAsync(comment);
        return id;
    }

    public async Task<IEnumerable<Comment>> ReadAllAsync()
    {
        var comments = await repository.ReadAllAsync();
        return comments;
    }

    public async Task<Comment> ReadAsync(int id)
    {
        Comment? comment = await repository.ReadAsync(id);
        return comment ?? throw new CommentNotFoundException("comment not found by id: " + id);
    }

    public async Task UpdateAsync(int id, Comment comment)
    {
        await ReadAsync(id);

        await repository.UpdateAsync(id, comment);
    }

    public async Task DeleteAsync(int id)
    {
        await ReadAsync(id);

        await repository.DeleteAsync(id);
    }
}