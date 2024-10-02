using DAL.Models;

namespace DAL.Data;

public interface ICommentRepository
{
    Task<int> CreateAsync(Comment comment);
    Task<IEnumerable<Comment>> ReadAllAsync();
    Task<Comment?> ReadAsync(int id);
    Task UpdateAsync(int id, Comment comment);
    Task DeleteAsync(int id);
}