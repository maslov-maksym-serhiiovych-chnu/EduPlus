using DAL.Models;

namespace BLL.Services;

public interface ICommentService
{
    Task<int> CreateAsync(Comment comment);
    Task<IEnumerable<Comment>> ReadAllAsync();
    Task<Comment> ReadAsync(int id);
    Task UpdateAsync(int id, Comment comment);
    Task DeleteAsync(int id);
}