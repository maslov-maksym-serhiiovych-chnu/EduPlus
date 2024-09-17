namespace DAL.Models;

public class Comment
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Course Course { get; set; } = null!;
}