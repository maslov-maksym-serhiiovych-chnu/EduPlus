namespace BLL.DTOs;

public class CourseQueryParameters
{
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}