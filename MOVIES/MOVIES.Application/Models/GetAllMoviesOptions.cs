using System.Xml.XPath;

namespace MOVIES.Application.Models;

public class GetAllMoviesOptions
{
    public string? Title { get; set; } = string.Empty;
    public int? YearOfRelease { get; set; }
    public Guid? UserId { get; set; }
    public string? SortField { get; set; }
    public SortOrder SortOrder { get; set; }
}

public enum SortOrder
{
    Unsorted,
    Ascending,
    Descending
}
