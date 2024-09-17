namespace DAL.Models;

public class Course
{
    public int Id { get; set; }
    public List<Comment> Comments { get; set; } = [];
}