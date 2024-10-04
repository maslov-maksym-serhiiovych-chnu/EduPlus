using DAL.Models;

namespace DAL.Data;

public interface ICourseRepository
{
    Task<int> CreateAsync(Course course);
    Task<IEnumerable<Course>> ReadAllAsync();
    Task<Course?> ReadAsync(int id);
    Task UpdateAsync(int id, Course course);
    Task DeleteAsync(int id);
}