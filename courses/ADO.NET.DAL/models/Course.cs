namespace ADO.NET.DAL.models;

public class Course
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<CourseUser> CourseUsers { get; set; } = new List<CourseUser>();
}