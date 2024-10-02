using BLL.Exceptions;
using DAL.Data;
using DAL.Models;

namespace BLL.Services;

public class CommentService(ICommentRepository repository) : ICommentService
{
    public async Task<int> CreateAsync(Comment comment)
    {
        return await repository.CreateAsync(comment);
    }

    public async Task<IEnumerable<Comment>> ReadAllAsync()
    {
        return await repository.ReadAllAsync();
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