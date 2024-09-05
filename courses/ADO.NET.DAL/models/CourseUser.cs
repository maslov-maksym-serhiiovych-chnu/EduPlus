namespace ADO.NET.DAL.models;

public class CourseUser
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}