namespace MOVIES.Application.Models;

public class GetAllMoviesOptions
{
    public string? Title { get; set; } = string.Empty;
    public int? YearOfRelease { get; set; }
    public Guid? UserId { get; set; }
}
