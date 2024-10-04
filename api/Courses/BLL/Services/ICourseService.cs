using DAL.Models;

namespace BLL.Services;

public interface ICourseService
{
    Task<int> CreateAsync(Course course);
    Task<IEnumerable<Course>> ReadAllAsync();
    Task<Course> ReadAsync(int id);
    Task UpdateAsync(int id, Course course);
    Task DeleteAsync(int id);
}