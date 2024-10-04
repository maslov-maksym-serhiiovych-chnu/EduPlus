using DAL.Models;

namespace BLL.Services;

public interface ICourseService
{
    Task<int> Create(Course course);
    Task<IEnumerable<Course>> ReadAll();
    Task<Course?> Read(int id);
    Task Update(int id, Course course);
    Task Delete(int id);
}